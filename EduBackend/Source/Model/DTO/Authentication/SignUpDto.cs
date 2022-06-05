using System.ComponentModel.DataAnnotations;

namespace EduBackend.Source.Model.DTO.Authentication;

public class SignUpDto
{
  [StringLength(512, MinimumLength = 1)]
  public string Username { get; set; } = null!;

  [EmailAddress, StringLength(512)]
  public string Email { get; set; } = null!;

  [StringLength(512, MinimumLength = 6)]
  public string Password { get; set; } = null!;
}