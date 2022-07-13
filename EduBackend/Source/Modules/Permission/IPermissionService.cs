using EduBackend.Source.Model.DTO.Permission;

namespace EduBackend.Source.Modules.Permission;

public interface IPermissionService
{
  Task<IEnumerable<string>> GetPermissionNamesByUserId(long userId);

  Task<IEnumerable<PermissionDto>> GetAllPermissions();

  Task ValidatePermissions(string[] permissions);

  Task<IEnumerable<Model.Entity.Permission>> AddPermissionsToRoleById(
    long roleId,
    string[] permissions);

  Task ReplacePermissionsByRoleId(long roleId, string[] permissions);
}