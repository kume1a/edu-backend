using EduBackend.Source.Model.DTO.Common;
using EduBackend.Source.Model.DTO.Role;

namespace EduBackend.Source.Modules.Role;

public interface IRoleService
{
  Task<RoleDto> CreateRole(string name, string[] permissions);
  Task<RoleDto> UpdateRoleById(long roleId, string? name = null, string[]? permissions = null);
  Task<RoleDto> GetRoleById(long id);
  Task<DataPageDto<RoleDto>> FilterRoles(int page, int pageSize);
}