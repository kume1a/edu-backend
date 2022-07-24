namespace EduBackend.Source.Modules.Author;

public interface IAuthorRepository
{
  Task<Model.Entity.Author> CreateEntity(string name, string filePath, string blurFilePath);
}