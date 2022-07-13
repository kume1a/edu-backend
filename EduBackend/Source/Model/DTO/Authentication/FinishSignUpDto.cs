using System.ComponentModel.DataAnnotations;
using EduBackend.Source.Model.Enum;

namespace EduBackend.Source.Model.DTO.Authentication;

public class FinishSignUpDto
{
  [RegularExpression(@"(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$")]
  public string Uuid { get; set; }

  [StringLength(512, MinimumLength = 1)]
  public string FirstName { get; set; }

  [StringLength(512, MinimumLength = 1)]
  public string LastName { get; set; }

  public DateTime BirthDate { get; set; }

  [EnumDataType(typeof(Gender))]
  public Gender Gender { get; set; }

  [StringLength(512, MinimumLength = 6)]
  public string Password { get; set; }
}