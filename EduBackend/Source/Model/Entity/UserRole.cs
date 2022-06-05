using Microsoft.AspNetCore.Identity;

namespace EduBackend.Source.Model.Entity;

public class UserRole : IdentityUserRole<long>
{
  public User User { get; set; }

  public Role Role { get; set; }
}