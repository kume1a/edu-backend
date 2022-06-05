using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EduBackend.Source.Exception.Http;
using EduBackend.Source.Model.Common;
using Microsoft.IdentityModel.Tokens;

namespace EduBackend.Source.Modules.Authentication;

public class JwtTokenService
{
  private readonly SigningCredentials _accessTokenSigningCredentials;
  private readonly int _accessTokenExpirationInMinutes;

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
    if (!didParseAccessTokenExpiration)
    {
      throw new InternalServerException();
    }
  }

  public string GenerateAccessToken(AuthenticationTokenPayload payload)
  {
    var claims = new List<Claim>
    {
      new(ClaimTypes.Email, payload.Email),
      new(ClaimTypes.NameIdentifier, payload.UserId.ToString()),
    };

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = DateTime.Now.AddMinutes(_accessTokenExpirationInMinutes),
      SigningCredentials = _accessTokenSigningCredentials,
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    var token = tokenHandler.CreateToken(tokenDescriptor);

    return tokenHandler.WriteToken(token);
  }
}