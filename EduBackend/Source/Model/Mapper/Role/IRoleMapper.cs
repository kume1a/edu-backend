using EduBackend.Source.Model.DTO.Role;

namespace EduBackend.Source.Model.Mapper.Role;

public interface IRoleMapper
{
  RoleDto DeepMap(Entity.Role role);
}