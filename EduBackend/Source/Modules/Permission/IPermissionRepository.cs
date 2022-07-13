using EduBackend.Source.Security;

namespace EduBackend.Source.Modules.Permission;

public interface IPermissionRepository
{
  Task<IEnumerable<string>> GetClaimValuesByUserId(long userId);

  Task<IEnumerable<Model.Entity.Permission>> CreatePermissionsWithRoleId(
    long roleId,
    IEnumerable<AppPermission> permissions);

  Task<IEnumerable<Model.Entity.Permission>> ReplacePermissionsByRoleId(
    long roleId,
    IEnumerable<AppPermission> permissions);
}