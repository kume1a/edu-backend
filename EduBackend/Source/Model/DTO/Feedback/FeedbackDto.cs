using EduBackend.Source.Model.Enum;

namespace EduBackend.Source.Model.DTO.Feedback;

public class FeedbackDto
{
  public long Id { get; set; }

  public string Content { get; set; }

  public Review Review { get; set; }

  public long UserId { get; set; }
  
  public string Username { get; set; }

  public DateTime CreatedAt { get; set; }
}