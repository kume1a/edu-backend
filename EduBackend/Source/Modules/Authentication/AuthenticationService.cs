using EduBackend.Source.Exception;
using EduBackend.Source.Exception.Http;
using EduBackend.Source.Model.Common;
using EduBackend.Source.Model.DTO.Authentication;
using EduBackend.Source.Modules.User;
using Microsoft.AspNetCore.Identity;

namespace EduBackend.Source.Modules.Authentication;

public class AuthenticationService : IAuthenticationService
{
  private readonly SignInManager<Model.Entity.User> _signInManager;
  private readonly JwtTokenService _jwtTokenService;
  private readonly IUserService _userService;

  public AuthenticationService
  (
    SignInManager<Model.Entity.User> signInManager,
    JwtTokenService jwtTokenService,
    IUserService userService)
  {
    _signInManager = signInManager;
    _jwtTokenService = jwtTokenService;
    _userService = userService;
  }

  public async Task<AuthenticationPayloadDto> SignIn(string email, string password)
  {
    var user = await _userService.GetUserByEmail(email);

    var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);
    if (!signInResult.Succeeded)
    {
      throw new UnauthorizedException(ExceptionMessageCode.InvalidEmailOrPassword);
    }

    var tokenPayload = new AuthenticationTokenPayload(user.Email, user.Id);
    var accessToken = _jwtTokenService.GenerateAccessToken(tokenPayload);
    var refreshToken = _jwtTokenService.GenerateRefreshToken(tokenPayload);

    await _userService.AddRefreshTokenByUserId(user.Id, refreshToken);

    return new AuthenticationPayloadDto
    {
      AccessToken = accessToken,
      RefreshToken = refreshToken
    };
  }

  public async Task<AuthenticationPayloadDto> SignUp(string username, string email, string password)
  {
    await _userService.ValidateDuplicateEmail(email);
    await _userService.ValidateDuplicateUsername(username);

    var user = await _userService.CreateUser(username, email, password);

    var tokenPayload = new AuthenticationTokenPayload(user.Email, user.Id);
    var accessToken = _jwtTokenService.GenerateAccessToken(tokenPayload);
    var refreshToken = _jwtTokenService.GenerateRefreshToken(tokenPayload);
    
    await _userService.AddRefreshTokenByUserId(user.Id, refreshToken);

    return new AuthenticationPayloadDto
    {
      AccessToken = accessToken,
      RefreshToken = refreshToken
    };
  }
}