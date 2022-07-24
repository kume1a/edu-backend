using EduBackend.Source.Model.DTO.Author;
using EduBackend.Source.Model.DTO.Common;

namespace EduBackend.Source.Modules.Author;

public interface IAuthorService
{
  Task<AuthorDto> CreateAuthor(string name, IFormFile image);
  Task<AuthorDto> UpdateAuthor(long authorId, string? name, IFormFile? image);
  Task DeleteAuthorById(long id);
  Task<DataPageDto<AuthorDto>> FilterAuthors(int page, int pageSize, string? searchQuery);
  Task<AuthorDto> GetAuthorById(long id);
}