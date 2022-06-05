namespace EduBackend.Source.Modules.Permission;

public class PermissionService : IPermissionService
{
  private readonly IPermissionRepository _permissionRepository;

  public PermissionService(IPermissionRepository permissionRepository)
  {
    _permissionRepository = permissionRepository;
  }

  public Task<IEnumerable<string>> GetPermissionNamesByUserId(long userId) =>
    _permissionRepository.GetNamesByUserId(userId);
}