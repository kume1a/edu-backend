using EduBackend.Source.Model;
using EduBackend.Source.Model.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace EduBackend.Source.Security;

public static class Startup
{
  public static IServiceCollection AddSecurityModule(this IServiceCollection services)
  {
    services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
      .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>()
      .AddIdentityCore<User>(
        options =>
        {
          options.Password.RequireNonAlphanumeric = false;
          options.Password.RequireDigit = false;
          options.Password.RequireUppercase = false;
        }
      )
      .AddRoles<Role>()
      .AddRoleManager<RoleManager<Role>>()
      .AddSignInManager<SignInManager<User>>()
      .AddRoleValidator<RoleValidator<Role>>()
      .AddEntityFrameworkStores<DataContext>();

    return services;
  }
}