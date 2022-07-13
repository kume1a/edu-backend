using EduBackend.Source.Model.Enum;
using EduBackend.Source.Model.Projection;

namespace EduBackend.Source.Modules.User;

public interface IUserService
{
  Task<Model.Entity.User> CreateUser(
    string firstName,
    string lastName,
    string email,
    DateTime birthDate,
    Gender gender,
    string password);
  Task ValidateDuplicateEmail(string email);
  Task<Model.Entity.User> GetUserByEmail(string email);
  Task AddRefreshTokenByUserId(long userId, string refreshToken);
  Task<UserIdEmailProjection?> GetUserIdByRefreshToken(string refreshToken);
  Task ClearRefreshTokensByUserId(long userId);
  Task DeleteRefreshToken(string refreshToken);
  Task UpdateUserPasswordByEmail(string email, string password);
  Task ValidateUserByEmail(string email);
}