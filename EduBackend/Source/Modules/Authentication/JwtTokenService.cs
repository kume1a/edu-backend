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
  private readonly SigningCredentials _accessTokenSigningCredentials;
  private readonly int _accessTokenExpirationInMinutes;
  private readonly SigningCredentials _refreshTokenSigningCredentials;
  private readonly int _refreshTokenExpirationInMinutes;

  public JwtTokenService(IConfiguration configuration)
  {
    var accessTokenKey = new SymmetricSecurityKey(
      Encoding.UTF8.GetBytes(configuration["JwtConfig:AccessTokenSecret"])
    );
    _accessTokenSigningCredentials = new SigningCredentials(
      accessTokenKey,
      SecurityAlgorithms.HmacSha512Signature
    );
    var didParseAccessTokenExpiration = int.TryParse(
      configuration["JwtConfig:AccessTokenExpirationInMinutes"],
      out _accessTokenExpirationInMinutes
    );

    var refreshTokenKey = new SymmetricSecurityKey(
      Encoding.UTF8.GetBytes(configuration["JwtConfig:RefreshTokenSecret"])
    );
    _refreshTokenSigningCredentials = new SigningCredentials(
      refreshTokenKey,
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