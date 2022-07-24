using EduBackend.Source.Common.Helper;

namespace EduBackend.Source.Common;

public static class Startup
{
  public static IServiceCollection AddCommonModule(this IServiceCollection services)
  {
    services.AddScoped<IEmailClient, EmailClient>()
      .AddScoped<IImageTypeResolver, ImageTypeResolver>()
      .AddScoped<IImageWriter, ImageWriter>()
      .AddScoped<IImageMutator, ImageMutator>();

    return services;
  }
}