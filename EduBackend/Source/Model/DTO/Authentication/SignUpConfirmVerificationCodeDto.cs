using System.ComponentModel.DataAnnotations;

namespace EduBackend.Source.Model.DTO.Authentication;

public class SignUpConfirmVerificationCodeDto
{
  [EmailAddress, StringLength(512)]
  public string Email { get; set; }

  public string Code { get; set; }
}