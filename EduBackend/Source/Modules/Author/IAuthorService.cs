using EduBackend.Source.Model.DTO.Author;

namespace EduBackend.Source.Modules.Author;

public interface IAuthorService
{
  Task<AuthorDto> CreateAuthor(string name, IFormFile image);
  Task<AuthorDto> UpdateAuthor(long authorId, string? name, IFormFile? image);
  Task DeleteAuthorById(long id);
}