using EduBackend.Source.Common;
using EduBackend.Source.Model;
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
}