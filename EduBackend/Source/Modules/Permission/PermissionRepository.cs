using EduBackend.Source.Model;
using Microsoft.EntityFrameworkCore;

namespace EduBackend.Source.Modules.Permission;

public class PermissionRepository: IPermissionRepository
{
  private readonly DataContext _db;

  public PermissionRepository(DataContext db)
  {
    _db = db;
  }
  
  public async Task<IEnumerable<string>> GetNamesByUserId(long userId)
  {
    return await _db.UserRoles.Where(userRole => userRole.UserId == userId)
        .Include(userRole => userRole.Role)
        .ThenInclude(role => role.RolePermissions)
        .ThenInclude(rolePermission => rolePermission.Permission)
        .SelectMany(
          userRole => userRole.Role.RolePermissions.Select(rp => rp.Permission.Name)
        )
        .ToListAsync();
  }
}