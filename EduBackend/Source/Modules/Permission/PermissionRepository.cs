using EduBackend.Source.Model;
using EduBackend.Source.Model.DTO.Common;
using EduBackend.Source.Model.DTO.Permission;
using Microsoft.EntityFrameworkCore;

namespace EduBackend.Source.Modules.Permission;

public class PermissionRepository : IPermissionRepository
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

  public async Task<DataPageDto<PermissionDto>> Filter(int page, int pageSize)
  {
    var query = _db.Permissions
      .OrderByDescending(permission => permission.CreatedAt)
      .Select(
        permission => new PermissionDto
        {
          Id = permission.Id,
          Permission = permission.Name,
          Description = permission.Description
        }
      );

    return await DataPageDto<PermissionDto>.fromQuery(
      query,
      page,
      pageSize
    );
  }
}