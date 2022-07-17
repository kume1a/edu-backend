namespace EduBackend.Source.Model.DTO.Document;

public class DocumentParagraphDto
{
  public long Id { get; set; }

  public long DocumentId { get; set; }

  public string Title { get; set; }

  public string Content { get; set; }

  public int Index { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }
}