using System.ComponentModel.DataAnnotations;

namespace EduBackend.Source.Model.DTO.Authentication;

public class RecoverPasswordDto
{
  [RegularExpression( @"(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$")]
  public string Uuid { get; set; }

  [StringLength(512, MinimumLength = 6)]
  public string NewPassword { get; set; }
}