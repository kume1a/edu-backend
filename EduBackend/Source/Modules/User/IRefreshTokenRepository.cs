using EduBackend.Source.Model.Entity.Projection;

namespace EduBackend.Source.Modules.User;

public interface IRefreshTokenRepository
{
  Task AddRefreshTokenByUserId(long userId, string refreshToken);
  Task<UserIdEmailProjection?> GetUserIdByValue(string refreshToken);
  Task DeleteAllByUserId(long userId);
  Task DeleteByValue(string value);
}