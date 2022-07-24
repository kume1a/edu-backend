using EduBackend.Source.Common.Helper;

namespace EduBackend.Source.Common;

public static class Startup
{
  public static IServiceCollection AddCommonModule(this IServiceCollection services)
  {
    services.AddScoped<IEmailClient, EmailClient>();

    return services;
  }
}