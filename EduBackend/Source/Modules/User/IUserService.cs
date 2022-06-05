namespace EduBackend.Source.Modules.User;

public interface IUserService
{
  public Task<Model.Entity.User> CreateUser(string username, string email, string password);

  public Task ValidateDuplicateEmail(string email);

  public Task ValidateDuplicateUsername(string username);
  public Task<Model.Entity.User> GetUserByEmail(string email);
}