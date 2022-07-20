using System.ComponentModel.DataAnnotations;
using EduBackend.Source.Model.Enum;

namespace EduBackend.Source.Model.DTO.Feedback;

public class CreateFeedbackDto
{
  [StringLength(2048, MinimumLength = 1)]
  public string Content { get; set; }

  [EnumDataType(typeof(Review))]
  public Review Review { get; set; }
}