namespace EduBackend.Source.Model.DTO.Author;

public class AuthorDto
{
  public long Id { get; set; }

  public string Name { get; set; }

  public DateTime CreatedAt { get; set; }

  public string ImagePath { get; set; }

  public string BlurImagePath { get; set; }
}