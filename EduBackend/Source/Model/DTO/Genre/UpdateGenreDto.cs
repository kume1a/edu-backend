using System.ComponentModel.DataAnnotations;

namespace EduBackend.Source.Model.DTO.Genre;

public class UpdateGenreDto
{
  [StringLength(512, MinimumLength = 1)]
  public string? Name { get; set; }
}