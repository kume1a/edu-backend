namespace EduBackend.Source.Modules.User;

public interface IUserRepository
{
  public Task<Model.Entity.User> CreateEntity(string username, string email, string password);
  Task<bool> ExistsByEmail(string email);

  Task<bool> ExistsByUsername(string username);
  Task<Model.Entity.User?> GetByEmail(string email);
}