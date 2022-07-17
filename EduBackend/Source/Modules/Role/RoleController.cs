using EduBackend.Source.Model.DTO.Common;
using EduBackend.Source.Model.DTO.Role;
using EduBackend.Source.Security;
using Microsoft.AspNetCore.Mvc;

namespace EduBackend.Source.Modules.Role;

[ApiController]
[Route("[controller]")]
public class RoleController : ControllerBase
{
  private readonly IRoleService _roleService;

  public RoleController(IRoleService roleService)
  {
    _roleService = roleService;
  }

  [HttpPost]
  [RequirePermission(AppAction.Create, AppResource.Roles)]
  public async Task<ActionResult<RoleDto>> CreateRole([FromBody] CreateRoleDto body)
  {
    var role = await _roleService.CreateRole(body.Name, body.Description, body.Permissions);

    return Created(role.Id.ToString(), role);
  }

  [HttpPatch("{id}")]
  [RequirePermission(AppAction.Update, AppResource.Roles)]
  public async Task<ActionResult<RoleDto>> UpdateRole(
    [FromRoute] long id,
    [FromBody] UpdateRoleDto body)
  {
    var updatedRole = await _roleService.UpdateRoleById(
      id,
      name: body.Name,
      description: body.Description,
      permissions: body.Permissions
    );

    return Ok(updatedRole);
  }

  [HttpGet]
  [RequirePermission(AppAction.Read, AppResource.Roles)]
  public async Task<ActionResult<DataPageDto<RoleDto>>> GetRoles([FromQuery] FilterRolesDto query)
  {
    var roles = await _roleService.FilterRoles(query.SearchQuery, query.Page, query.PageSize);

    return Ok(roles);
  }

  [HttpGet("{id}")]
  [RequirePermission(AppAction.Read, AppResource.Roles)]
  public async Task<ActionResult<RoleDto>> GetRole([FromRoute] long id)
  {
    var role = await _roleService.GetRoleById(id);

    return Ok(role);
  }
}