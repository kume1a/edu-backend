using EduBackend.Source.Model;
using EduBackend.Source.Model.Common;
using Microsoft.EntityFrameworkCore;

namespace EduBackend.Source.Modules.Genre;

public class GenreRepository : IGenreRepository
{
  private readonly DataContext _db;

  public GenreRepository(DataContext db)
  {
    _db = db;
  }

  public async Task<Model.Entity.Genre> CreateEntity(string name)
  {
    var entity = new Model.Entity.Genre
    {
      Name = name
    };

    await _db.Genres.AddAsync(entity);
    await _db.SaveChangesAsync();

    return entity;
  }

  public async Task<bool> DeleteById(long id)
  {
    var entity = await _db.Genres.SingleOrDefaultAsync(genre => genre.Id == id);
    if (entity is null)
    {
      return false;
    }

    _db.Genres.Remove(entity);
    await _db.SaveChangesAsync();

    return true;
  }

  public async Task<DataPage<Model.Entity.Genre>> Filter(
    int page,
    int pageSize,
    string? searchQuery)
  {
    IQueryable<Model.Entity.Genre> query = _db.Genres
      .AsNoTracking()
      .OrderByDescending(genre => genre.CreatedAt);

    if (searchQuery is not null)
    {
      query = query.Where(
        role => EF.Functions.Like(role.Name.ToUpper(), $"%{searchQuery.ToUpper()}%")
      );
    }

    return await DataPage<Model.Entity.Genre>.FromQuery(query, page, pageSize);
  }

  public async Task<Model.Entity.Genre?> UpdateById(long id, string? name)
  {
    var entity = await _db.Genres.SingleOrDefaultAsync(genre => genre.Id == id);
    if (entity is null)
    {
      return null;
    }

    if (name is not null) entity.Name = name;

    _db.Genres.Update(entity);
    await _db.SaveChangesAsync();

    return entity;
  }

  public async Task<Model.Entity.Genre?> GetById(long id)
  {
    return await _db.Genres
      .AsNoTracking()
      .SingleOrDefaultAsync(genre => genre.Id == id);
  }
}