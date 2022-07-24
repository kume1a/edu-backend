namespace EduBackend.Source.Model.DTO.Author;

public class CreateAuthorDto
{
  public string Name { get; set; }

  public IFormFile Image { get; set; }
}