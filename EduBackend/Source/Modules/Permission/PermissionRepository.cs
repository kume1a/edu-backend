using System.Collections.Immutable;
using EduBackend.Source.Common;
using EduBackend.Source.Model;
using EduBackend.Source.Security;
using Microsoft.EntityFrameworkCore;

namespace EduBackend.Source.Modules.Permission;

public class PermissionRepository : IPermissionRepository
{
  private readonly DataContext _db;

  public PermissionRepository(DataContext db)
  {
    _db = db;
  }

  public async Task<IEnumerable<string>> GetClaimValuesByUserId(long userId)
  {
    return await _db.UserRoles.Where(userRole => userRole.UserId == userId)
      .Include(userRole => userRole.Role)
      .ThenInclude(role => role.Permissions)
      .SelectMany(
        userRole => userRole.Role.Permissions
          .Where(rolePermission => rolePermission.ClaimType == AppClaimTypes.Permission)
          .Select(permission => permission.ClaimValue)
      )
      .ToListAsync();
  }

  public async Task<IEnumerable<Model.Entity.Permission>> CreatePermissionsWithRoleId(
    long roleId,
    IEnumerable<AppPermission> permissions)
  {
    var entities = permissions.Select(
        permission => new Model.Entity.Permission
        {
          Description = permission.Description,
          ClaimType = AppClaimTypes.Permission,
          ClaimValue = permission.Name,
          RoleId = roleId
        }
      )
      .ToImmutableArray();

    await _db.Permissions.AddRangeAsync(entities);
    await _db.SaveChangesAsync();

    return entities;
  }

  public async Task<IEnumerable<Model.Entity.Permission>> ReplacePermissionsByRoleId(
    long roleId,
    IEnumerable<AppPermission> permissions)
  {
    var oldPermissions =
      await _db.Permissions.Where(permission => permission.RoleId == roleId)
        .ToListAsync();

    var newPermissions = permissions.Select(
        permission => new Model.Entity.Permission
        {
          Description = permission.Description,
          ClaimType = AppClaimTypes.Permission,
          ClaimValue = permission.Name,
          RoleId = roleId
        }
      )
      .ToList();

    _db.RemoveRange(oldPermissions);
    await _db.Permissions.AddRangeAsync(newPermissions);
    await _db.SaveChangesAsync();

    return newPermissions;
  }
}