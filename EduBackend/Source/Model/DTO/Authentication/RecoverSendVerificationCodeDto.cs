using System.ComponentModel.DataAnnotations;

namespace EduBackend.Source.Model.DTO.Authentication;

public class RecoverSendVerificationCodeDto
{
  [EmailAddress, StringLength(512)]
  public string Email { get; set; }
}