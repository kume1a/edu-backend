using EduBackend.Source.Model;
using EduBackend.Source.Model.Common;
using EduBackend.Source.Model.Projection;
using Microsoft.EntityFrameworkCore;

namespace EduBackend.Source.Modules.Author;

public class AuthorRepository : IAuthorRepository
{
  private readonly DataContext _db;

  public AuthorRepository(DataContext db)
  {
    _db = db;
  }

  public async Task<Model.Entity.Author> CreateEntity(
    string name,
    string imagePath,
    string blurImagePath)
  {
    var entity = new Model.Entity.Author
    {
      Name = name,
      ImagePath = imagePath,
      BlurImagePath = blurImagePath
    };

    await _db.Authors.AddAsync(entity);
    await _db.SaveChangesAsync();

    return entity;
  }

  public async Task<Model.Entity.Author?> UpdateById(
    long authorId,
    string? name,
    string? imagePath,
    string? blurImagePath)
  {
    var entity = await _db.Authors.SingleOrDefaultAsync(e => e.Id == authorId);
    if (entity is null)
    {
      return null;
    }

    if (name is not null) entity.Name = name;
    if (imagePath is not null) entity.ImagePath = imagePath;
    if (blurImagePath is not null) entity.BlurImagePath = blurImagePath;

    _db.Authors.Update(entity);
    await _db.SaveChangesAsync();

    return entity;
  }

  public async Task<AuthorImagesProjection?> GetImagePathsById(long authorId)
  {
    return await _db.Authors
      .AsNoTracking()
      .Where(e => e.Id == authorId)
      .Select(
        e => new AuthorImagesProjection
        {
          ImagePath = e.ImagePath,
          BlurImagePath = e.BlurImagePath
        }
      )
      .SingleOrDefaultAsync();
  }

  public async Task<bool> ExistsById(long id)
  {
    return await _db.Authors.AnyAsync(e => e.Id == id);
  }

  public async Task<bool> DeleteById(long id)
  {
    var entity = await _db.Authors.SingleOrDefaultAsync(e => e.Id == id);
    if (entity is null)
    {
      return false;
    }

    _db.Authors.Remove(entity);
    await _db.SaveChangesAsync();

    return true;
  }

  public async Task<DataPage<Model.Entity.Author>> Filter(
    int page,
    int pageSize,
    string? searchQuery)
  {
    IQueryable<Model.Entity.Author> query = _db.Authors
      .AsNoTracking()
      .OrderByDescending(role => role.CreatedAt);

    if (searchQuery is not null)
    {
      query = query.Where(
        role => EF.Functions.Like(role.Name.ToUpper(), $"%{searchQuery.ToUpper()}%")
      );
    }

    return await DataPage<Model.Entity.Author>.FromQuery(query, page, pageSize);
  }

  public async Task<Model.Entity.Author?> GetById(long id)
  {
    return await _db.Authors
      .AsNoTracking()
      .SingleOrDefaultAsync(e => e.Id == id);
  }

  public async Task<IEnumerable<Model.Entity.Author>> GetAll()
  {
    return await _db.Authors
      .AsNoTracking()
      .OrderByDescending(e => e.CreatedAt)
      .Select(e => new Model.Entity.Author { Id = e.Id, Name = e.Name })
      .ToListAsync();
  }
}