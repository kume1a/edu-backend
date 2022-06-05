using Microsoft.AspNetCore.Authorization;

namespace EduBackend.Source.Security;

internal class PermissionRequirement : IAuthorizationRequirement
{
  public string Permission { get; }

  public PermissionRequirement(string permission)
  {
    Permission = permission;
  }
}