using EduBackend.Source.Model.DTO.Role;
using EduBackend.Source.Model.Entity;

namespace EduBackend.Source.Model.Mapper.Role;

public interface IRoleClaimMapper
{
  RoleClaimDto shallowMap(Permission permission);
}