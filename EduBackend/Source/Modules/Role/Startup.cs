namespace EduBackend.Source.Modules.Role;

public static class Startup
{
  public static IServiceCollection AddRoleModule(this IServiceCollection services)
  {
    services.AddScoped<IRoleRepository, RoleRepository>()
      .AddScoped<IRoleService, RoleService>();

    return services;
  }
}