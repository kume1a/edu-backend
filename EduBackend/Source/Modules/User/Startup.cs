namespace EduBackend.Source.Modules.User;

public static class Startup
{
  public static IServiceCollection AddUserModule(this IServiceCollection services) =>
    services.AddScoped<IUserRepository, UserRepository>()
      .AddScoped<IUserService, UserService>()
      .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
}