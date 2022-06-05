using System.ComponentModel.DataAnnotations;

namespace EduBackend.Source.Model.DTO.Common;

public class PageOptionsDto
{
  [Range(1, int.MaxValue)]
  public int Page { get; set; }

  [Range(1, 100)]
  public int PageSize { get; set; }
}