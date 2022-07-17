using EduBackend.Source.Model.DTO.Common;

namespace EduBackend.Source.Model.DTO.Genre;

public class FilterGenresDto: PageOptionsDto
{
  public string? SearchQuery { get; set; }
}