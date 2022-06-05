using Microsoft.OpenApi.Models;

namespace EduBackend.Source.Config;

public static class SwaggerConfig
{
  public static IServiceCollection AddSwaggerModule(this IServiceCollection services)
  {
    services.AddEndpointsApiExplorer()
      .AddSwaggerGen(
        options =>
        {
          options.SwaggerDoc("v1", new OpenApiInfo { Title = "Edu", Version = "v1" });
          options.AddSecurityDefinition(
            "Bearer",
            new OpenApiSecurityScheme
            {
              Name = "Authorization",
              In = ParameterLocation.Header,
              Type = SecuritySchemeType.ApiKey,
              Scheme = "Bearer",
              BearerFormat = "JWT"
            }
          );

          options.AddSecurityRequirement(
            new OpenApiSecurityRequirement
            {
              {
                new OpenApiSecurityScheme
                {
                  Reference = new OpenApiReference
                  {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                  }
                },
                new string[] { }
              }
            }
          );
        }
      );
    
    return services;
  } 
}