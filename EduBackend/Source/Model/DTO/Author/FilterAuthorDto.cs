using EduBackend.Source.Model.DTO.Common;

namespace EduBackend.Source.Model.DTO.Author;

public class FilterAuthorDto: PageOptionsDto
{
  public string? SearchQuery { get; set; }
}