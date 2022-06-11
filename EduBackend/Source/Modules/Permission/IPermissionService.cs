using EduBackend.Source.Model.DTO.Permission;

namespace EduBackend.Source.Modules.Permission;

public interface IPermissionService
{
  Task<IEnumerable<string>> GetPermissionNamesByUserId(long userId);

  Task<IEnumerable<PermissionDto>> GetAllPermissions();
}