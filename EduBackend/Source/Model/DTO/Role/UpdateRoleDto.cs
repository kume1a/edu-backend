using System.ComponentModel.DataAnnotations;

namespace EduBackend.Source.Model.DTO.Role;

public class UpdateRoleDto
{
  [StringLength(512, MinimumLength = 1)]
  public string? Name { get; set; }
  
  [StringLength(512, MinimumLength = 1)]
  public string? Description { get; set; }

  [MinLength(1)]
  public string[]? Permissions { get; set; }
}