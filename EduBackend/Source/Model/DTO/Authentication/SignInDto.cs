using System.ComponentModel.DataAnnotations;

namespace EduBackend.Source.Model.DTO.Authentication;

public class SignInDto
{
  [EmailAddress, StringLength(512)]
  public string Email { get; set; } = null!;

  [StringLength(512, MinimumLength = 6)]
  public string Password { get; set; } = null!;
}