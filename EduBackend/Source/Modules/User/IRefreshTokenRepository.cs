namespace EduBackend.Source.Modules.User;

public interface IRefreshTokenRepository
{
  Task AddRefreshTokenByUserId(long userId, string refreshToken);
}