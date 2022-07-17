using System.ComponentModel.DataAnnotations;

namespace EduBackend.Source.Model.DTO.Genre;

public class CreateGenreDto
{
  [StringLength(512, MinimumLength = 1)]
  public string Name { get; set; }
}