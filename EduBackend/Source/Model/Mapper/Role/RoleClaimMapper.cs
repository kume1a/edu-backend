using EduBackend.Source.Model.DTO.Role;
using EduBackend.Source.Model.Entity;

namespace EduBackend.Source.Model.Mapper.Role;

public class RoleClaimMapper : IRoleClaimMapper
{
  public RoleClaimDto shallowMap(Permission permission)
  {
    return new RoleClaimDto
    {
      Id = permission.Id,
      Description = permission.Description,
      ClaimValue = permission.ClaimValue,
      CreatedAt = permission.CreatedAt,
      RoleId = permission.RoleId
    };
  }
}