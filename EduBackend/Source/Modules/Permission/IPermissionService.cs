using EduBackend.Source.Model.DTO.Common;
using EduBackend.Source.Model.DTO.Permission;

namespace EduBackend.Source.Modules.Permission;

public interface IPermissionService
{
  public Task<IEnumerable<string>> GetPermissionNamesByUserId(long userId);

  public Task<IEnumerable<PermissionDto>> GetAllPermissions();
}