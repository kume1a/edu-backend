using EduBackend.Source.Model.DTO.Common;
using EduBackend.Source.Model.DTO.Role;
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
  public async Task<ActionResult<RoleDto>> CreateRole([FromBody] CreateRoleDto body)
  {
    var role = await _roleService.CreateRole(body.Name, body.Permissions);

    return Created(role.Id.ToString(), role);
  }

  [HttpPatch("{id}")]
  public async Task<ActionResult<RoleDto>> UpdateRole(
    [FromRoute] long id,
    [FromBody] UpdateRoleDto body)
  {
    var updatedRole = await _roleService.UpdateRoleById(
      id,
      name: body.Name,
      permissions: body.Permissions
    );

    return Ok(updatedRole);
  }
  
  [HttpGet]
  public async Task<ActionResult<DataPageDto<RoleDto>>> GetRoles([FromQuery] FilterRolesDto query)
  {
    var role = await _roleService.FilterRoles(query.Page, query.PageSize);

    return Ok(role);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<RoleDto>> GetRole([FromRoute] long id)
  {
    var role = await _roleService.GetRoleById(id);

    return Ok(role);
  }
}