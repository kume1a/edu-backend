using EduBackend.Source.Model.Enum;

namespace EduBackend.Source.Model.Entity;

public class Feedback
{
  public long Id { get; set; }

  public Review Review { get; set; }

  public string Content { get; set; }

  public DateTime CreatedAt { get; set; }

  public long UserId { get; set; }

  public User User { get; set; }
}