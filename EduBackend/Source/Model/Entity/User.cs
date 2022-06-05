using Microsoft.AspNetCore.Identity;

namespace EduBackend.Source.Model.Entity;

public class User : IdentityUser<long>
{
  public ICollection<UserRole> UserRoles { get; set; }
}