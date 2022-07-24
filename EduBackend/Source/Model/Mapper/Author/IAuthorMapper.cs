using EduBackend.Source.Model.DTO.Author;

namespace EduBackend.Source.Model.Mapper.Author;

public interface IAuthorMapper
{
  AuthorDto ShallowMap(Entity.Author author);
}