using EduBackend.Source.Model;

namespace EduBackend.Source.Modules.Author;

public class AuthorRepository : IAuthorRepository
{
  private readonly DataContext _db;

  public AuthorRepository(DataContext db)
  {
    _db = db;
  }
  
  public async Task<Model.Entity.Author> CreateEntity(string name, string imagePath, string blurImagePath)
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
}