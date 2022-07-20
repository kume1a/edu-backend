using EduBackend.Source.Model.Common;
using EduBackend.Source.Model.Enum;

namespace EduBackend.Source.Modules.Feedback;

public interface IFeedbackRepository
{
  Task<Model.Entity.Feedback> CreateEntity(long userId, string content, Review review);
  Task<DataPage<Model.Entity.Feedback>> Filter(int page, int pageSize, string? searchQuery);
}