using EduBackend.Source.Model.DTO.Common;
using EduBackend.Source.Model.DTO.Feedback;
using EduBackend.Source.Model.Enum;

namespace EduBackend.Source.Modules.Feedback;

public interface IFeedbackService
{
  Task<FeedbackDto> CreateFeedback(long userId, string content, Review review);
  Task<DataPageDto<FeedbackDto>> FilterFeedback(int page, int pageSize, string? searchQuery);
}