using EduBackend.Source.Model.Entity.Projection;

namespace EduBackend.Source.Modules.User;

public interface IUserService
{
  Task<Model.Entity.User> CreateUser(string username, string email, string password);
  Task ValidateDuplicateEmail(string email);
  Task ValidateDuplicateUsername(string username);
  Task<Model.Entity.User> GetUserByEmail(string email);
  Task AddRefreshTokenByUserId(long userId, string refreshToken);
  Task<UserIdEmailProjection?> GetUserIdByRefreshToken(string refreshToken);
  Task ClearRefreshTokensByUserId(long userId);
  Task DeleteRefreshToken(string refreshToken);
}