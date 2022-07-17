using EduBackend.Source.Model.DTO.Common;
using EduBackend.Source.Model.DTO.Genre;

namespace EduBackend.Source.Modules.Genre;

public interface IGenreService
{
  Task<GenreDto> CreateGenre(string name);
  Task<GenreDto> UpdateGenreById(long id, string? name);
  Task<DataPageDto<GenreDto>> FilterGenres(int page, int pageSize, string? searchQuery);
  Task DeleteGenreById(long id);
  Task<GenreDto> GetGenreById(long id);
}