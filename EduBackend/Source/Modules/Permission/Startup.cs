namespace EduBackend.Source.Modules.Permission;

public static class Startup
{
  public static IServiceCollection AddPermissionModule(this IServiceCollection services) =>
    services.AddScoped<IPermissionRepository, PermissionRepository>()
      .AddScoped<IPermissionService, PermissionService>();
}