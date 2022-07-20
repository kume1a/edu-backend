using EduBackend.Source.Model.DTO.Common;
using EduBackend.Source.Model.DTO.Feedback;
using EduBackend.Source.Model.Enum;
using EduBackend.Source.Model.Mapper.Feedback;

namespace EduBackend.Source.Modules.Feedback;

public class FeedbackService : IFeedbackService
{
  private readonly IFeedbackRepository _feedbackRepository;
  private readonly IFeedbackMapper _feedbackMapper;

  public FeedbackService(IFeedbackRepository feedbackRepository, IFeedbackMapper feedbackMapper)
  {
    _feedbackRepository = feedbackRepository;
    _feedbackMapper = feedbackMapper;
  }

  public async Task<FeedbackDto> CreateFeedback(long userId, string content, Review review)
  {
    var feedback = await _feedbackRepository.CreateEntity(userId, content, review);

    return _feedbackMapper.ShallowMap(feedback);
  }

  public async Task<DataPageDto<FeedbackDto>> FilterFeedback(int page, int pageSize, string? searchQuery)
  {
    var feedbackPage = await _feedbackRepository.Filter(page, pageSize, searchQuery);
    
    return DataPageDto<FeedbackDto>.fromDataPage(feedbackPage,  _feedbackMapper.DeepMap);
  }
}