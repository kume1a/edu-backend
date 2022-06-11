using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace EduBackend.Source.Modules.Authentication;

public static class Startup
{
  public static IServiceCollection AddAuthenticationModule
  (
    this IServiceCollection services,
    IConfiguration configuration)
  {
    services.AddScoped<JwtTokenService>()
      .AddScoped<IAuthenticationService, AuthenticationService>()
      .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(
        JwtBearerDefaults.AuthenticationScheme,
        options =>
        {
          options.SaveToken = true;
          options.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(configuration["JwtConfig:AccessTokenSecret"])
            ),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
          };
        }
      );

    return services;
  }
}