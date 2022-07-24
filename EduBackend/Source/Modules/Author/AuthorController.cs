using EduBackend.Source.Model.DTO.Author;
using EduBackend.Source.Model.DTO.Common;
using Microsoft.AspNetCore.Mvc;

namespace EduBackend.Source.Modules.Author;

[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
  private readonly IAuthorService _authorService;

  public AuthorController(IAuthorService authorService)
  {
    _authorService = authorService;
  }

  [HttpPost]
  public async Task<ActionResult<AuthorDto>> CreateAuthor([FromForm] CreateAuthorDto form)
  {
    var author = await _authorService.CreateAuthor(form.Name, form.Image);

    return Created(author.Id.ToString(), author);
  }

  [HttpPatch("{id}")]
  public async Task<ActionResult<AuthorDto>> UpdateAuthor(
    [FromRoute] long id,
    [FromForm] UpdateAuthorDto form)
  {
    var author = await _authorService.UpdateAuthor(id, form.Name, form.Image);

    return Ok(author);
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult> DeleteAuthor([FromRoute] long id)
  {
    await _authorService.DeleteAuthorById(id);

    return Ok();
  }

  [HttpGet]
  public async Task<ActionResult<DataPageDto<AuthorDto>>> FilterAuthors(
    [FromQuery] FilterAuthorDto query)
  {
    var authors = await _authorService.FilterAuthors(
      query.Page,
      query.PageSize,
      query.SearchQuery
    );

    return Ok(authors);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<AuthorDto>> GetAuthorById([FromRoute] long id)
  {
    var author = await _authorService.GetAuthorById(id);

    return Ok(author);
  }
}