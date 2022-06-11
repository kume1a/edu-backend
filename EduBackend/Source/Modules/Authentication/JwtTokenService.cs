using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EduBackend.Source.Common;
using EduBackend.Source.Exception.Http;
using EduBackend.Source.Model.Common;
using Microsoft.IdentityModel.Tokens;

namespace EduBackend.Source.Modules.Authentication;

public class JwtTokenService
{
  private readonly ILogger<JwtTokenService> _logger;

  private readonly SigningCredentials _accessTokenSigningCredentials;
  private readonly SecurityKey _accessTokenKey;
  private readonly int _accessTokenExpirationInMinutes;
  private readonly SigningCredentials _refreshTokenSigningCredentials;
  private readonly SecurityKey _refreshTokenKey;
  private readonly int _refreshTokenExpirationInMinutes;

  public JwtTokenService(IConfiguration configuration, ILogger<JwtTokenService> logger)
  {
    _logger = logger;

    _accessTokenKey = new SymmetricSecurityKey(
      Encoding.UTF8.GetBytes(configuration["JwtConfig:AccessTokenSecret"])
    );
    _accessTokenSigningCredentials = new SigningCredentials(
      _accessTokenKey,
      SecurityAlgorithms.HmacSha512Signature
    );
    var didParseAccessTokenExpiration = int.TryParse(
      configuration["JwtConfig:AccessTokenExpirationInMinutes"],
      out _accessTokenExpirationInMinutes
    );

    _refreshTokenKey = new SymmetricSecurityKey(
      Encoding.UTF8.GetBytes(configuration["JwtConfig:RefreshTokenSecret"])
    );
    _refreshTokenSigningCredentials = new SigningCredentials(
      _refreshTokenKey,
      SecurityAlgorithms.HmacSha512Signature
    );
    var didParseRefreshTokenExpiration = int.TryParse(
      configuration["JwtConfig:RefreshTokenExpirationInMinutes"],
      out _refreshTokenExpirationInMinutes
    );

    if (!didParseRefreshTokenExpiration || !didParseAccessTokenExpiration)
    {
      throw new InternalServerException();
    }
  }

  public AuthenticationTokenPayload? DecodeToken(string token)
  {
    var handler = new JwtSecurityTokenHandler();
    var jwtSecurityToken = handler.ReadJwtToken(token);

    var emailClaim =
      jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == AppClaimTypes.Email);
    var userIdClaim =
      jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == AppClaimTypes.UserId);

    if (emailClaim is null || userIdClaim is null)
    {
      return null;
    }

    return !long.TryParse(userIdClaim.Value, out var userId)
      ? null
      : new AuthenticationTokenPayload(emailClaim.Value, userId);
  }

  public bool ValidateRefreshToken(string token)
  {
    var validationParameters = new TokenValidationParameters
    {
      ValidateLifetime = true,
      ValidateAudience = false,
      ValidateIssuer = false,
      IssuerSigningKey = _refreshTokenKey
    };

    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

    try
    {
      handler.ValidateToken(token, validationParameters, out _);
      return true;
    }
    catch (System.Exception e)
    {
      _logger.LogError("Error validating token, {Message}, {StackTrace}", e.Message, e.StackTrace);
    }

    return false;
  }

  public string GenerateAccessToken(AuthenticationTokenPayload payload)
  {
    return GenerateJwtToken(
      payload,
      _accessTokenExpirationInMinutes,
      _accessTokenSigningCredentials
    );
  }

  public string GenerateRefreshToken(AuthenticationTokenPayload payload)
  {
    return GenerateJwtToken(
      payload,
      _refreshTokenExpirationInMinutes,
      _refreshTokenSigningCredentials
    );
  }

  private static string GenerateJwtToken
  (
    AuthenticationTokenPayload payload,
    int expirationInMinutes,
    SigningCredentials signingCredentials)
  {
    var claims = new List<Claim>
    {
      new(AppClaimTypes.Email, payload.Email),
      new(AppClaimTypes.UserId, payload.UserId.ToString()),
    };

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = DateTime.Now.AddMinutes(expirationInMinutes),
      SigningCredentials = signingCredentials
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    var token = tokenHandler.CreateToken(tokenDescriptor);

    return tokenHandler.WriteToken(token);
  }
}