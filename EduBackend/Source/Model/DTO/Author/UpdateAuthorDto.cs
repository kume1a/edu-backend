using System.ComponentModel.DataAnnotations;

namespace EduBackend.Source.Model.DTO.Author;

public class UpdateAuthorDto
{
  [StringLength(256, MinimumLength = 1)]
  public string? Name { get; set; }

  public IFormFile? Image { get; set; }
}