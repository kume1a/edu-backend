using EduBackend.Source.Model.DTO.Role;

namespace EduBackend.Source.Model.Mapper.Role;

public class RoleMapper: IRoleMapper
{
  private readonly IRoleClaimMapper _roleClaimMapper;

  public RoleMapper(IRoleClaimMapper roleClaimMapper)
  {
    _roleClaimMapper = roleClaimMapper;
  }
  
  public RoleDto DeepMap(Entity.Role role)
  {
    return new RoleDto
    {
      Id = role.Id,
      Name = role.Name,
      Description = role.Description,
      CreatedAt = role.CreatedAt,
      Claims = role.Permissions.Select(_roleClaimMapper.ShallowMap)
    };
  }
}