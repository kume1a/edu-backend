using EduBackend.Source.Model.Common;

namespace EduBackend.Source.Modules.Genre;

public interface IGenreRepository
{
  Task<Model.Entity.Genre> CreateEntity(string name);
  Task<bool> DeleteById(long id);
  Task<DataPage<Model.Entity.Genre>> Filter(int page, int pageSize, string? searchQuery);
  Task<Model.Entity.Genre?> UpdateById(long id, string? name);
  Task<Model.Entity.Genre?> GetById(long id);
}