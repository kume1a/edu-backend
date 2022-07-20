using EduBackend.Source.Model.DTO.Feedback;

namespace EduBackend.Source.Model.Mapper.Feedback;

public interface IFeedbackMapper
{
  FeedbackDto DeepMap(Entity.Feedback feedback);
  
  FeedbackDto ShallowMap(Entity.Feedback feedback);
}