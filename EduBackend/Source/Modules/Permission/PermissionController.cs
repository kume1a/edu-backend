using EduBackend.Source.Model.DTO.Common;
using EduBackend.Source.Model.DTO.Permission;
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
  public async Task<DataPageDto<PermissionDto>> GetPermissions
  (
    [FromQuery] FilterPermissionDto query)
  {
    return await _permissionService.FilterPermissions(query.Page, query.PageSize);
  }
}