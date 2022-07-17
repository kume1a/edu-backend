namespace EduBackend.Source.Model.Entity;

public class DocumentParagraph
{
  public long Id { get; set; }

  public string Title { get; set; }

  public int Index { get; set; }

  public string Content { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public long DocumentId { get; set; }

  public Document Document { get; set; }
}