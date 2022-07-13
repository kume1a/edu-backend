using EduBackend.Source.Model.Enum;

namespace EduBackend.Source.Modules.User;

public interface IUserRepository
{
  Task<Model.Entity.User> CreateEntity(
    string email,
    string username,
    DateTime birthDate,
    Gender gender,
    string password);
  Task<bool> ExistsByEmail(string email);
  Task<Model.Entity.User?> GetByEmail(string email);
  Task UpdatePasswordByEmail(string email, string password);
  Task<string?> GetEmailById(long userId);
}