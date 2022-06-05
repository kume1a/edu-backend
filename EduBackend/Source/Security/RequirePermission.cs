using Microsoft.AspNetCore.Authorization;

namespace EduBackend.Source.Security;

public class RequirePermission : AuthorizeAttribute
{
  public RequirePermission(string action, string resource) =>
    Policy = AppPermission.NameFor(action, resource);
}