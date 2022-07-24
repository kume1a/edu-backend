using EduBackend.Source.Model.Projection;

namespace EduBackend.Source.Modules.Author;

public interface IAuthorRepository
{
  Task<Model.Entity.Author> CreateEntity(string name, string imagePath, string blurImagePath);
  Task<Model.Entity.Author?> UpdateById(
    long authorId,
    string? name,
    string? imagePath,
    string? blurImagePath);

  Task<AuthorImagesProjection?> GetImagePathsById(long authorId);

  Task<bool> ExistsById(long id);
  Task<bool> DeleteById(long id);
}