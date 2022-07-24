using EduBackend.Source.Model.DTO.Author;
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
  public async Task<ActionResult<AuthorDto>> CreateAuthor(
    [FromRoute] long id,
    [FromForm] UpdateAuthorDto form)
  {
    var author = await _authorService.UpdateAuthor(id, form.Name, form.Image);

    return Ok(author);
  }
  
  [HttpDelete("{id}")]
  public async Task<ActionResult<AuthorDto>> DeleteAuthor([FromRoute] long id)
  {
    await _authorService.DeleteAuthorById(id);

    return Ok();
  }
}