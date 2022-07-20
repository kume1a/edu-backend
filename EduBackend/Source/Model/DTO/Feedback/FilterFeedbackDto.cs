using EduBackend.Source.Model.DTO.Common;

namespace EduBackend.Source.Model.DTO.Feedback;

public class FilterFeedbackDto: PageOptionsDto
{
  public string? SearchQuery { get; set; }
}