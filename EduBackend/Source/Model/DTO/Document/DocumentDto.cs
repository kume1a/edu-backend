namespace EduBackend.Source.Model.DTO.Document;

public class DocumentDto
{
  public long Id { get; set; }

  public string Title { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public IEnumerable<DocumentParagraphDto> Paragraphs { get; set; }
}