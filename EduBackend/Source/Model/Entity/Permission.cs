using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EduBackend.Source.Model.Entity;

public class Permission : IdentityRoleClaim<long>
{
  public string? Description { get; set; }

  [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
  public DateTime CreatedAt { get; set; }

  public Role Role { get; set; }
}