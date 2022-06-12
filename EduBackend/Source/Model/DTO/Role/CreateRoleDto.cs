using System.ComponentModel.DataAnnotations;

namespace EduBackend.Source.Model.DTO.Role;

public class CreateRoleDto
{
  [StringLength(512, MinimumLength = 1)]
  public string Name { get; set; }

  [MinLength(1)]
  public string[] Permissions { get; set; }
}