using EduBackend.Source.Common;
using EduBackend.Source.Exception;
using EduBackend.Source.Exception.Http;
using EduBackend.Source.Model.Common;
using EduBackend.Source.Model.DTO.Authentication;
using EduBackend.Source.Model.Enum;
using EduBackend.Source.Modules.Authentication.RecoverPasswordRequest;
using EduBackend.Source.Modules.Authentication.SignUpRequest;
using EduBackend.Source.Modules.User;
using Microsoft.AspNetCore.Identity;

namespace EduBackend.Source.Modules.Authentication;

public class AuthenticationService : IAuthenticationService
{
  private readonly SignInManager<Model.Entity.User> _signInManager;
  private readonly JwtTokenService _jwtTokenService;
  private readonly IUserService _userService;
  private readonly IRecoverPasswordRequestService _recoverPasswordRequestService;
  private readonly IEmailClient _emailClient;
  private readonly ISignUpRequestService _signUpRequestService;

  public AuthenticationService(
    SignInManager<Model.Entity.User> signInManager,
    JwtTokenService jwtTokenService,
    IUserService userService,
    IRecoverPasswordRequestService recoverPasswordRequestService,
    IEmailClient emailClient,
    ISignUpRequestService signUpRequestService)
  {
    _signInManager = signInManager;
    _jwtTokenService = jwtTokenService;
    _userService = userService;
    _recoverPasswordRequestService = recoverPasswordRequestService;
    _emailClient = emailClient;
    _signUpRequestService = signUpRequestService;
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

  public async Task<AuthenticationPayloadDto> Refresh(string refreshToken)
  {
    var userIdEmail = await _userService.GetUserIdByRefreshToken(refreshToken);
    if (userIdEmail is null)
    {
      var decodedPayload = _jwtTokenService.DecodeToken(refreshToken);
      if (decodedPayload is null)
      {
        throw new ForbiddenException(ExceptionMessageCode.RefreshTokenReuse);
      }

      await _userService.ClearRefreshTokensByUserId(decodedPayload.UserId);
      throw new ForbiddenException(ExceptionMessageCode.RefreshTokenReuse);
    }

    var tokenPayload = new AuthenticationTokenPayload(userIdEmail.Email, userIdEmail.UserId);
    var newAccessToken = _jwtTokenService.GenerateAccessToken(tokenPayload);
    var newRefreshToken = _jwtTokenService.GenerateAccessToken(tokenPayload);

    if (!_jwtTokenService.ValidateRefreshToken(refreshToken))
    {
      await _userService.DeleteRefreshToken(refreshToken);
      throw new ForbiddenException(ExceptionMessageCode.InvalidToken);
    }

    await _userService.DeleteRefreshToken(refreshToken);
    await _userService.AddRefreshTokenByUserId(userIdEmail.UserId, newRefreshToken);

    return new AuthenticationPayloadDto
    {
      AccessToken = newAccessToken,
      RefreshToken = newRefreshToken
    };
  }

  public async Task RecoverSendVerificationCode(string email)
  {
    await _userService.ValidateUserByEmail(email);

    var random = new Random();
    var code = random.Next(10000, 99999).ToString();

    await _recoverPasswordRequestService.CreateRecoverPasswordRequest(
      email,
      code,
      isVerified: false
    );

    await _emailClient.SendEmailAsync(email, "Recover password", code);
  }

  public async Task<RecoverConfirmVerificationCodeResponseDto> RecoverConfirmVerificationCode(
    string email,
    string code)
  {
    var recoverPasswordRequest =
      await _recoverPasswordRequestService.GetRecoverPasswordRequestByEmail(email);

    await _recoverPasswordRequestService.ValidateRecoverPasswordExpiration(recoverPasswordRequest);

    if (recoverPasswordRequest.Code != code)
    {
      throw new ForbiddenException(ExceptionMessageCode.InvalidVerificationCode);
    }

    var uuid = Guid.NewGuid().ToString();

    await _recoverPasswordRequestService.MarkRecoverPasswordRequestAsVerified(
      recoverPasswordRequest.Id,
      uuid
    );

    return new RecoverConfirmVerificationCodeResponseDto
    {
      Uuid = uuid
    };
  }

  public async Task RecoverPassword(string uuid, string newPassword)
  {
    var recoverPasswordRequest =
      await _recoverPasswordRequestService.GetRecoverPasswordRequestByUuid(uuid);
    if (!recoverPasswordRequest.IsVerified)
    {
      throw new BadRequestException(ExceptionMessageCode.RequestNotVerified);
    }

    await _userService.UpdateUserPasswordByEmail(recoverPasswordRequest.Email, newPassword);

    await _recoverPasswordRequestService.DeleteRecoverPasswordRequest(recoverPasswordRequest);
  }

  public async Task RequestSignUp(string email)
  {
    await _userService.ValidateDuplicateEmail(email);
    
    var random = new Random();
    var code = random.Next(10000, 99999).ToString();
    
    await _signUpRequestService.CreateSignUpRequest(email, code);

    await _emailClient.SendEmailAsync(email, "Sign up", code);
  }

  public async Task<SignUpConfirmVerificationCodeResponseDto> SignUpConfirmVerificationCode(
    string email,
    string code)
  {
    var signUpRequest = await _signUpRequestService.GetSignUpRequestByEmail(email);
    
    if (signUpRequest.Code != code)
    {
      throw new ForbiddenException(ExceptionMessageCode.InvalidVerificationCode);
    }

    var uuid = Guid.NewGuid().ToString();

    await _signUpRequestService.MarkSignUpRequestAsVerified(
      signUpRequest.Id,
      uuid
    );

    return new SignUpConfirmVerificationCodeResponseDto
    {
      Uuid = uuid
    };
  }

  public async Task<AuthenticationPayloadDto> FinishSignUp(
    string uuid,
    string firstName,
    string lastName,
    Gender gender,
    DateTime birthDate,
    string password)
  {
    var signUpRequest = await _signUpRequestService.GetSignUpRequestByUuid(uuid);
    if (!signUpRequest.IsVerified)
    {
      throw new BadRequestException(ExceptionMessageCode.RequestNotVerified);
    }

    var user = await _userService.CreateUser(
      firstName,
      lastName,
      signUpRequest.Email,
      birthDate,
      gender,
      password
    );

    var tokenPayload = new AuthenticationTokenPayload(user.Email, user.Id);
    var accessToken = _jwtTokenService.GenerateAccessToken(tokenPayload);
    var refreshToken = _jwtTokenService.GenerateRefreshToken(tokenPayload);

    await _userService.AddRefreshTokenByUserId(user.Id, refreshToken);
    await _signUpRequestService.DeleteSignUpRequest(signUpRequest);

    return new AuthenticationPayloadDto
    {
      AccessToken = accessToken,
      RefreshToken = refreshToken
    };
  }
}