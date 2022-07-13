using EduBackend.Source.Model;
using EduBackend.Source.Model.Common;
using Microsoft.EntityFrameworkCore;

namespace EduBackend.Source.Modules.Role;

public class RoleRepository : IRoleRepository
{
  private readonly DataContext _db;

  public RoleRepository(DataContext db)
  {
    _db = db;
  }

  public async Task<bool> ExistsByName(string name)
  {
    return await _db.Roles.AnyAsync(role => role.Name == name);
  }

  public async Task<bool> ExistsOtherByName(long roleId, string name)
  {
    return await _db.Roles.AnyAsync(role => role.Name == name && role.Id != roleId);
  }

  public async Task<Model.Entity.Role> CreateEntity(string name, string description)
  {
    var role = new Model.Entity.Role
    {
      Name = name,
      NormalizedName = name.ToUpper(),
      Description = description
    };

    await _db.Roles.AddAsync(role);
    await _db.SaveChangesAsync();

    return role;
  }

  public async Task<Model.Entity.Role?> UpdateById(long id, string? name, string? description)
  {
    var role = await _db.Roles
      .Where(role => role.Id == id)
      .Include(permission => permission.Permissions)
      .FirstOrDefaultAsync();

    if (role is null)
    {
      return null;
    }

    if (name is not null)
    {
      role.Name = name;
      role.NormalizedName = name;
    }

    if (description is not null) role.Description = description;

    _db.Roles.Update(role);
    await _db.SaveChangesAsync();

    return role;
  }

  public Task<Model.Entity.Role?> GetById(long id)
  {
    return _db.Roles.Where(role => role.Id == id)
      .Include(role => role.Permissions)
      .SingleOrDefaultAsync();
  }

  public async Task<DataPage<Model.Entity.Role>> Filter(string? searchQuery, int page, int pageSize)
  {
    IQueryable<Model.Entity.Role> query = _db.Roles
      .Include(role => role.Permissions)
      .OrderByDescending(role => role.CreatedAt);

    if (searchQuery is not null)
    {
      query = query.Where(role => EF.Functions.Like(role.Name.ToUpper(), $"%{searchQuery.ToUpper()}%"));
    }

    return await DataPage<Model.Entity.Role>.FromQuery(query, page, pageSize);
  }
}