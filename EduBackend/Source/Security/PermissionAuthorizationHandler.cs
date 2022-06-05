using System.Security.Claims;
using EduBackend.Source.Exception;
using EduBackend.Source.Exception.Http;
using EduBackend.Source.Modules.Permission;
using Microsoft.AspNetCore.Authorization;

namespace EduBackend.Source.Security;

internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
  private readonly IPermissionService _permissionService;

  public PermissionAuthorizationHandler(IPermissionService permissionService)
  {
    _permissionService = permissionService;
  }

  protected override async Task HandleRequirementAsync
  (
    AuthorizationHandlerContext context,
    PermissionRequirement requirement)
  {
    var userIdPayload = context.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)
      ?.Value;
    if (userIdPayload is null)
    {
      throw new ForbiddenException(ExceptionMessageCode.InvalidToken);
    }

    var didParseUserId = int.TryParse(userIdPayload, out var userId);
    if (!didParseUserId)
    {
      throw new ForbiddenException(ExceptionMessageCode.InvalidToken);
    }

    var userPermissions = await _permissionService.GetPermissionNamesByUserId(userId);
    if (!userPermissions.Contains(requirement.Permission))
    {
      throw new ForbiddenException(ExceptionMessageCode.PermissionDenied);
    }

    context.Succeed(requirement);
  }
}