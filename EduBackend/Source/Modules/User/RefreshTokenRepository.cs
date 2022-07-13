using EduBackend.Source.Model;
using EduBackend.Source.Model.Entity;
using EduBackend.Source.Model.Projection;
using Microsoft.EntityFrameworkCore;

namespace EduBackend.Source.Modules.User;

public class RefreshTokenRepository : IRefreshTokenRepository
{
  private readonly DataContext _db;

  public RefreshTokenRepository(DataContext db)
  {
    _db = db;
  }

  public async Task AddRefreshTokenByUserId(long userId, string refreshToken)
  {
    await _db.RefreshTokens.AddAsync(
      new RefreshToken
      {
        Value = refreshToken,
        UserId = userId
      }
    );
    await _db.SaveChangesAsync();
  }

  public async Task<UserIdEmailProjection?> GetUserIdByValue(string refreshToken)
  {
    return await _db.RefreshTokens
      .Where(rt => rt.Value == refreshToken)
      .Include(rt => rt.User)
      .Select(rt => new UserIdEmailProjection { UserId = rt.UserId, Email = rt.User.Email })
      .FirstOrDefaultAsync();
  }

  public async Task DeleteAllByUserId(long userId)
  {
    var refreshTokens =
      await _db.RefreshTokens.Where(token => token.UserId == userId).ToListAsync();
    _db.RefreshTokens.RemoveRange(refreshTokens);
    await _db.SaveChangesAsync();
  }

  public async Task DeleteByValue(string value)
  {
    var entity = await _db.RefreshTokens.Where(rt => rt.Value == value).SingleOrDefaultAsync();
    if (entity is null)
    {
      return;
    }

    _db.RefreshTokens.Remove(entity);
    await _db.SaveChangesAsync();
  }
}