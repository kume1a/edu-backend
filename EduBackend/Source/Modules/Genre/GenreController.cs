using EduBackend.Source.Model.DTO.Common;
using EduBackend.Source.Model.DTO.Genre;
using Microsoft.AspNetCore.Mvc;

namespace EduBackend.Source.Modules.Genre;

[ApiController]
[Route("[controller]")]
public class GenreController : ControllerBase
{
  private readonly IGenreService _genreService;

  public GenreController(IGenreService genreService)
  {
    _genreService = genreService;
  }

  [HttpPost]
  public async Task<ActionResult<GenreDto>> CreateGenre([FromBody] CreateGenreDto body)
  {
    var genre = await _genreService.CreateGenre(body.Name);

    return Created(genre.Id.ToString(), genre);
  }

  [HttpPatch("{id}")]
  public async Task<ActionResult<GenreDto>> UpdateGenre(
    [FromRoute] long id,
    [FromBody] UpdateGenreDto body)
  {
    var genre = await _genreService.UpdateGenreById(id, body.Name);

    return Ok(genre);
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult> DeleteGenre([FromRoute] long id)
  {
    await _genreService.DeleteGenreById(id);

    return Ok();
  }

  [HttpGet]
  public async Task<ActionResult<DataPageDto<GenreDto>>> GetGenres(
    [FromQuery] FilterGenresDto query)
  {
    var genres =
      await _genreService.FilterGenres(query.Page, query.PageSize, query.SearchQuery);

    return Ok(genres);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<DataPageDto<GenreDto>>> GetGenre([FromRoute] long id)
  {
    var genre = await _genreService.GetGenreById(id);

    return Ok(genre);
  }
}