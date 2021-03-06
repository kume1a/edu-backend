using EduBackend.Source.Model.DTO.Permission;
using EduBackend.Source.Security;
using Microsoft.AspNetCore.Mvc;

namespace EduBackend.Source.Modules.Permission;

[ApiController]
[Route("[controller]")]
public class PermissionController : ControllerBase
{
  private readonly IPermissionService _permissionService;

  public PermissionController(IPermissionService permissionService)
  {
    _permissionService = permissionService;
  }

  [HttpGet]
  [RequirePermission(AppAction.Read, AppResource.Permissions)]
  public async Task<IEnumerable<PermissionDto>> GetAllPermissions()
  {
    return await _permissionService.GetAllPermissions();
  }
}