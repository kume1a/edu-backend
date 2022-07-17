using EduBackend.Source.Model.Enum;

namespace EduBackend.Source.Model.Entity;

public class Document
{
  public long Id { get; set; }

  public string Title { get; set; }

  public DocumentType DocumentType { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public IEnumerable<DocumentParagraph> Paragraphs { get; set; }
}