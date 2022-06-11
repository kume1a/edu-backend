using EduBackend.Source.Model.DTO.Common;
using EduBackend.Source.Model.DTO.Permission;

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

  public Task<DataPageDto<PermissionDto>> FilterPermissions(int page, int pageSize)
  {
    return _permissionRepository.Filter(page, pageSize);
  }
}