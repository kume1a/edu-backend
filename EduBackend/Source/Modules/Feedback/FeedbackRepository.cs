using EduBackend.Source.Model;
using EduBackend.Source.Model.Common;
using EduBackend.Source.Model.Enum;
using Microsoft.EntityFrameworkCore;

namespace EduBackend.Source.Modules.Feedback;

public class FeedbackRepository : IFeedbackRepository
{
  private readonly DataContext _db;

  public FeedbackRepository(DataContext db)
  {
    _db = db;
  }

  public async Task<Model.Entity.Feedback> CreateEntity(long userId, string content, Review review)
  {
    var entity = new Model.Entity.Feedback
    {
      Content = content,
      Review = review,
      UserId = userId,
    };

    await _db.Feedback.AddAsync(entity);
    await _db.SaveChangesAsync();

    return entity;
  }

  public async Task<DataPage<Model.Entity.Feedback>> Filter(int page, int pageSize, string? searchQuery)
  {
    IQueryable<Model.Entity.Feedback> query = _db.Feedback
      .Include(e => e.User)
      .Select(e => new Model.Entity.Feedback
      {
        Id = e.Id,
        Content = e.Content,
        User = new Model.Entity.User
        {
          Username = e.User.Username
        },
        Review = e.Review,
        CreatedAt = e.CreatedAt,
        UserId = e.UserId
      })
      .OrderByDescending(role => role.CreatedAt);

    if (searchQuery is not null)
    {
      query = query.Where(e => EF.Functions.Like(e.Content.ToUpper(), $"%{searchQuery.ToUpper()}%"));
    }

    return await DataPage<Model.Entity.Feedback>.FromQuery(query, page, pageSize);
  }
}