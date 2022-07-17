using EduBackend.Source.Model.DTO.Genre;

namespace EduBackend.Source.Model.Mapper.Genre;

public class GenreMapper: IGenreMapper
{
  public GenreDto ShallowMap(Entity.Genre genre)
  {
    return new GenreDto
    {
      Id = genre.Id,
      Name = genre.Name,
      CreatedAt = genre.CreatedAt
    };
  }
}