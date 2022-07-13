using EduBackend.Source.Exception;
using EduBackend.Source.Exception.Http;
using EduBackend.Source.Model.Enum;
using EduBackend.Source.Model.Projection;

namespace EduBackend.Source.Modules.User;

public class UserService : IUserService
{
  private readonly IUserRepository _userRepository;
  private readonly IRefreshTokenRepository _refreshTokenRepository;

  public UserService(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
  {
    _userRepository = userRepository;
    _refreshTokenRepository = refreshTokenRepository;
  }

  public Task<Model.Entity.User> CreateUser(
    string firstName,
    string lastName,
    string email,
    DateTime birthDate,
    Gender gender,
    string password)
  {
    return _userRepository.CreateEntity(firstName, lastName, email, birthDate, gender, password);
  }

  public async Task ValidateDuplicateEmail(string email)
  {
    if (await _userRepository.ExistsByEmail(email))
    {
      throw new ConflictException(ExceptionMessageCode.EmailAlreadyInUse);
    }
  }

  public async Task<Model.Entity.User> GetUserByEmail(string email)
  {
    var user = await _userRepository.GetByEmail(email);
    if (user is null)
    {
      throw new UnauthorizedException(ExceptionMessageCode.InvalidEmailOrPassword);
    }

    return user;
  }

  public async Task AddRefreshTokenByUserId(long userId, string refreshToken)
  {
    await _refreshTokenRepository.AddRefreshTokenByUserId(userId, refreshToken);
  }

  public async Task<UserIdEmailProjection?> GetUserIdByRefreshToken(string refreshToken)
  {
    return await _refreshTokenRepository.GetUserIdByValue(refreshToken);
  }

  public async Task ClearRefreshTokensByUserId(long userId)
  {
    await _refreshTokenRepository.DeleteAllByUserId(userId);
  }

  public async Task DeleteRefreshToken(string refreshToken)
  {
    await _refreshTokenRepository.DeleteByValue(refreshToken);
  }

  public async Task UpdateUserPasswordByEmail(string email, string password)
  {
    await _userRepository.UpdatePasswordByEmail(email, password);
  }

  public async Task ValidateUserByEmail(string email)
  {
    if (!await _userRepository.ExistsByEmail(email))
    {
      throw new NotFoundException(ExceptionMessageCode.UserNotFound);
    }
  }
}