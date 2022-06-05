using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EduBackend.Source.Modules.User;

public class UserRepository : IUserRepository
{
  private readonly UserManager<Model.Entity.User> _userManager;
  private readonly ILogger<UserRepository> _logger;

  public UserRepository
  (
    UserManager<Model.Entity.User> userManager,
    ILogger<UserRepository> logger)
  {
    _userManager = userManager;
    _logger = logger;
  }

  public async Task<Model.Entity.User> CreateEntity(string username, string email, string password)
  {
    var user = new Model.Entity.User
    {
      UserName = username,
      Email = email,
    };

    var createResult = await _userManager.CreateAsync(user, password);
    if (!createResult.Succeeded)
    {
      _logger.LogError(
        "error creating user: {ErrorDescription}",
        String.Join("\n", createResult.Errors.Select(e => e.Description))
      );
    }

    return user;
  }

  public async Task<bool> ExistsByEmail(string email)
  {
    return await _userManager.Users.AnyAsync(user => user.Email == email);
  }

  public async Task<bool> ExistsByUsername(string username)
  {
    return await _userManager.Users.AnyAsync(user => user.UserName == username);
  }

  public async Task<Model.Entity.User?> GetByEmail(string email)
  {
    return await _userManager.Users.SingleOrDefaultAsync(user => user.Email == email);
  }
}