using Microsoft.AspNetCore.Identity;

namespace EduBackend.Source.Model.Entity;

public class Role : IdentityRole<long>
{
  public ICollection<UserRole> UserRoles { get; set; }

  public ICollection<Permission> Permissions { get; set; }
}