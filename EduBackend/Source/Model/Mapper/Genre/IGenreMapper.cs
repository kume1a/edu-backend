using EduBackend.Source.Model.DTO.Genre;

namespace EduBackend.Source.Model.Mapper.Genre;

public interface IGenreMapper
{
  GenreDto ShallowMap(Entity.Genre genre);
}