using System.ComponentModel.DataAnnotations;

namespace EduBackend.Source.Model.DTO.Document;

public class CreateDocumentParagraphDto
{
  [StringLength(512, MinimumLength = 1)]
  public string Title { get; set; }

  [Range(1, int.MaxValue)]
  public int Index { get; set; }

  [StringLength(4096, MinimumLength = 1)]
  public string Content { get; set; }
}