using EduBackend.Source.Exception.Http;
using EduBackend.Source.Model.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EduBackend.Source.Modules.User;

public class UserRepository : IUserRepository
{
  private readonly UserManager<Model.Entity.User> _userManager;
  private readonly ILogger<UserRepository> _logger;

  public UserRepository(
    UserManager<Model.Entity.User> userManager,
    ILogger<UserRepository> logger)
  {
    _userManager = userManager;
    _logger = logger;
  }

  public async Task<Model.Entity.User> CreateEntity(string email, string username, DateTime birthDate, Gender gender,
    string password)
  {
    var user = new Model.Entity.User
    {
      Email = email,
      Username = username,
      UserName = email,
      BirthDate = birthDate,
      Gender = gender,
    };

    var createResult = await _userManager.CreateAsync(user, password);
    if (createResult.Succeeded) return user;

    _logger.LogError(
      "error creating user: {ErrorDescription}",
      String.Join("\n", createResult.Errors.Select(e => e.Description))
    );
    throw new InternalServerException();
  }

  public async Task<bool> ExistsByEmail(string email)
  {
    return await _userManager.Users.AnyAsync(user => user.Email == email);
  }

  public async Task<Model.Entity.User?> GetByEmail(string email)
  {
    return await _userManager.Users.SingleOrDefaultAsync(user => user.Email == email);
  }

  public async Task UpdatePasswordByEmail(string email, string password)
  {
    var user = await _userManager.FindByEmailAsync(email);
    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
    var result = await _userManager.ResetPasswordAsync(user, token, password);

    if (!result.Succeeded)
    {
      throw new InternalServerException();
    }
  }

  public async Task<string?> GetEmailById(long userId)
  {
    return await _userManager.Users
      .Where(user => user.Id == userId)
      .Select(user => user.Email)
      .SingleOrDefaultAsync();
  }
}