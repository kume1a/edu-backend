using EduBackend.Source.Model.DTO.Permission;
using EduBackend.Source.Security;

namespace EduBackend.Source.Modules.Permission;

public class PermissionService : IPermissionService
{
  private readonly IPermissionRepository _permissionRepository;

  public PermissionService(IPermissionRepository permissionRepository)
  {
    _permissionRepository = permissionRepository;
  }

  public Task<IEnumerable<string>> GetPermissionNamesByUserId(long userId) =>
    _permissionRepository.GetClaimValuesByUserId(userId);

  public async Task<IEnumerable<PermissionDto>> GetAllPermissions()
  {
    return await Task.Run(
      () => AppPermissions.All.Select(
        permission => new PermissionDto
        {
          Description = permission.Description,
          Permission = permission.Name,
          Action = permission.Action,
          Resource = permission.Resource,
        }
      )
    );
  }
}