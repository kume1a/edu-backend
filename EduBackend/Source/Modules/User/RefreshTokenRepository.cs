using EduBackend.Source.Model;
using EduBackend.Source.Model.Entity;

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
}