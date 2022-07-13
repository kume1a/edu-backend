using System.Collections.Immutable;
using EduBackend.Source.Exception;
using EduBackend.Source.Exception.Http;
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

  public Task ValidatePermissions(string[] permissions)
  {
    var appPermissionValues =
      AppPermissions.All.Select(permission => permission.Name).ToImmutableArray();

    if (permissions.Any(permission => !appPermissionValues.Contains(permission)))
    {
      throw new BadRequestException(ExceptionMessageCode.InvalidPermission);
    }

    return Task.CompletedTask;
  }

  public Task<IEnumerable<Model.Entity.Permission>> AddPermissionsToRoleById(
    long roleId,
    string[] permissions)
  {
    return _permissionRepository.CreatePermissionsWithRoleId(
      roleId,
      PermissionsToAppPermissions(permissions)
    );
  }

  public async Task ReplacePermissionsByRoleId(long roleId, string[] permissions)
  {
    await _permissionRepository.ReplacePermissionsByRoleId(
      roleId,
      PermissionsToAppPermissions(permissions)
    );
  }

  private static AppPermission[] PermissionsToAppPermissions(string[] permissions)
  {
    return permissions.Select(
        permission =>
          AppPermissions.All.FirstOrDefault(appPermission => permission == appPermission.Name)
      )
      .Where(appPermission => appPermission is not null)
      .OfType<AppPermission>()
      .ToArray();
  }
}