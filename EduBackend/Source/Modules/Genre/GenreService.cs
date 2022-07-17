using EduBackend.Source.Exception;
using EduBackend.Source.Exception.Http;
using EduBackend.Source.Model.DTO.Common;
using EduBackend.Source.Model.DTO.Genre;
using EduBackend.Source.Model.Mapper.Genre;

namespace EduBackend.Source.Modules.Genre;

public class GenreService : IGenreService
{
  private readonly IGenreRepository _genreRepository;
  private readonly IGenreMapper _genreMapper;

  public GenreService(IGenreRepository genreRepository, IGenreMapper genreMapper)
  {
    _genreRepository = genreRepository;
    _genreMapper = genreMapper;
  }
  
  public async Task<GenreDto> CreateGenre(string name)
  {
    var genre = await _genreRepository.CreateEntity(name);

    return _genreMapper.ShallowMap(genre);
  }

  public async Task<GenreDto> UpdateGenreById(long id, string? name)
  {
    var genre = await _genreRepository.UpdateById(id, name);
    if (genre is null)
    {
      throw new NotFoundException(ExceptionMessageCode.GenreNotFound);
    }

    return _genreMapper.ShallowMap(genre);
  }

  public async Task<DataPageDto<GenreDto>> FilterGenres(int page, int pageSize, string? searchQuery)
  {
    var genres = await _genreRepository.Filter(page, pageSize, searchQuery);
    
    return DataPageDto<GenreDto>.fromDataPage(genres, _genreMapper.ShallowMap);
  }

  public async Task DeleteGenreById(long id)
  {
    var didDelete = await _genreRepository.DeleteById(id);
    if (!didDelete)
    {
      throw new NotFoundException(ExceptionMessageCode.GenreNotFound);
    }
  }

  public async Task<GenreDto> GetGenreById(long id)
  {
    var genre = await _genreRepository.GetById(id);
    if (genre is null)
    {
      throw new NotFoundException(ExceptionMessageCode.GenreNotFound);
    }

    return _genreMapper.ShallowMap(genre);
  }
}