using EduBackend.Source.Exception;
using EduBackend.Source.Exception.Http;

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

  public Task<Model.Entity.User> CreateUser(string username, string email, string password)
  {
    return _userRepository.CreateEntity(username, email, password);
  }

  public async Task ValidateDuplicateEmail(string email)
  {
    if (await _userRepository.ExistsByEmail(email))
    {
      throw new ConflictException(ExceptionMessageCode.EmailAlreadyInUse);
    }
  }

  public async Task ValidateDuplicateUsername(string username)
  {
    if (await _userRepository.ExistsByUsername(username))
    {
      throw new ConflictException(ExceptionMessageCode.UsernameAlreadyInUse);
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
}