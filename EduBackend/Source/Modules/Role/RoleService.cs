using EduBackend.Source.Exception;
using EduBackend.Source.Exception.Http;
using EduBackend.Source.Model.DTO.Common;
using EduBackend.Source.Model.DTO.Role;
using EduBackend.Source.Model.Mapper.Role;
using EduBackend.Source.Modules.Permission;

namespace EduBackend.Source.Modules.Role;

public class RoleService : IRoleService
{
  private readonly IRoleRepository _roleRepository;
  private readonly IPermissionService _permissionService;
  private readonly IRoleMapper _roleMapper;

  public RoleService(
    IRoleRepository roleRepository,
    IPermissionService permissionService,
    IRoleMapper roleMapper)
  {
    _roleRepository = roleRepository;
    _permissionService = permissionService;
    _roleMapper = roleMapper;
  }

  async Task<RoleDto> IRoleService.CreateRole(string name, string description, string[] permissions)
  {
    await _permissionService.ValidatePermissions(permissions);

    if (await _roleRepository.ExistsByName(name))
    {
      throw new ConflictException(ExceptionMessageCode.RoleNameAlreadyExists);
    }

    var roleEntity = await _roleRepository.CreateEntity(name, description);

    await _permissionService.AddPermissionsToRoleById(roleEntity.Id, permissions);

    return _roleMapper.DeepMap(roleEntity);
  }

  public async Task<RoleDto> UpdateRoleById(
    long roleId,
    string? name = null,
    string? description = null,
    string[]? permissions = null)
  {
    if (permissions is not null)
    {
      await _permissionService.ValidatePermissions(permissions);
    }

    if (name is not null && await _roleRepository.ExistsOtherByName(roleId, name))
    {
      throw new ConflictException(ExceptionMessageCode.RoleNameAlreadyExists);
    }

    var roleEntity = await _roleRepository.UpdateById(roleId, name, description);
    if (roleEntity is null)
    {
      throw new NotFoundException(ExceptionMessageCode.RoleNotFound);
    }

    if (permissions is not null)
    {
      await _permissionService.ReplacePermissionsByRoleId(roleId, permissions);
    }

    return _roleMapper.DeepMap(roleEntity);
  }

  public async Task<RoleDto> GetRoleById(long id)
  {
    var role = await _roleRepository.GetById(id);
    if (role is null)
    {
      throw new NotFoundException(ExceptionMessageCode.RoleNotFound);
    }

    return _roleMapper.DeepMap(role);
  }

  public async Task<DataPageDto<RoleDto>> FilterRoles(string? searchQuery, int page, int pageSize)
  {
    var roles = await _roleRepository.Filter(searchQuery, page, pageSize);

    var mapped = roles.Data.Select(_roleMapper.DeepMap).ToList();

    return new DataPageDto<RoleDto>(page, pageSize, roles.TotalCount, roles.TotalPages, mapped);
  }
}