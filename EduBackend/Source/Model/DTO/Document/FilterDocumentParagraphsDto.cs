using System.ComponentModel.DataAnnotations;
using EduBackend.Source.Model.DTO.Common;

namespace EduBackend.Source.Model.DTO.Document;

public class FilterDocumentParagraphsDto: PageOptionsDto
{
  [StringLength(maximumLength: 512, MinimumLength = 1)]
  public string SearchQuery { get; set; }
}