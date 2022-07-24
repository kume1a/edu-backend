namespace EduBackend.Source.Model.Entity;

public class Author
{
  public long Id { get; set; }

  public DateTime CreatedAt { get; set; }

  public string Name { get; set; }

  public string ImagePath { get; set; }

  public string BlurImagePath { get; set; }
}