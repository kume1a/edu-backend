using EduBackend.Source.Model.Enum;
using Microsoft.AspNetCore.Identity;

namespace EduBackend.Source.Model.Entity;

public class User : IdentityUser<long>
{
  public string Username { get; set; }

  public DateTime BirthDate { get; set; }

  public Gender Gender { get; set; }
  
  public ICollection<UserRole> UserRoles { get; set; }

  public AccountVerificationCode AccountVerificationCode { get; set; }
}