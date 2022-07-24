using EduBackend.Source.Model.DTO.Author;

namespace EduBackend.Source.Model.Mapper.Author;

public class AuthorMapper : IAuthorMapper
{
  public AuthorDto ShallowMap(Entity.Author author)
  {
    return new AuthorDto
    {
      Id = author.Id,
      CreatedAt = author.CreatedAt,
      Name = author.Name,
      ImagePath = author.ImagePath,
      BlurImagePath = author.BlurImagePath,
    };
  }
}