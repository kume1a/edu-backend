using System.ComponentModel.DataAnnotations;
using EduBackend.Source.Model.DTO.Common;

namespace EduBackend.Source.Model.DTO.Role;

public class FilterRolesDto : PageOptionsDto
{
  [StringLength(maximumLength: 512, MinimumLength = 1)]
  public string? searchQuery { get; set; }
}