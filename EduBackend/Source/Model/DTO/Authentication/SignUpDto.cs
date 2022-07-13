using System.ComponentModel.DataAnnotations;
using EduBackend.Source.Model.Enum;

namespace EduBackend.Source.Model.DTO.Authentication;

public class SignUpDto
{
  [EmailAddress, StringLength(512)]
  public string Email { get; set; }
  
  [StringLength(512, MinimumLength = 1)]
  public string Username { get; set; }

  public DateTime BirthDate { get; set; }

  [EnumDataType(typeof(Gender))]
  public Gender Gender { get; set; }

  [StringLength(512, MinimumLength = 6)]
  public string Password { get; set; }
}