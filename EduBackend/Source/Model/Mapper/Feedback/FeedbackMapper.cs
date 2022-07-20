using EduBackend.Source.Model.DTO.Feedback;

namespace EduBackend.Source.Model.Mapper.Feedback;

public class FeedbackMapper : IFeedbackMapper
{
  public FeedbackDto DeepMap(Entity.Feedback feedback)
  {
    return new FeedbackDto
    {
      Id = feedback.Id,
      Content = feedback.Content,
      Review = feedback.Review,
      CreatedAt = feedback.CreatedAt,
      UserId = feedback.UserId,
      Username = feedback.User.Username
    };
  }

  public FeedbackDto ShallowMap(Entity.Feedback feedback)
  {
    return new FeedbackDto
    {
      Id = feedback.Id,
      Content = feedback.Content,
      Review = feedback.Review,
      CreatedAt = feedback.CreatedAt,
      UserId = feedback.UserId,
    };
  }
}