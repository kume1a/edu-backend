using EduBackend.Source.Model.DTO.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduBackend.Source.Modules.Authentication;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class AuthenticationController : ControllerBase
{
  private readonly IAuthenticationService _authenticationService;

  public AuthenticationController(IAuthenticationService authenticationService)
  {
    _authenticationService = authenticationService;
  }

  [HttpPost("SignUp/Request")]
  public async Task<ActionResult> RequestSignUp([FromBody] RequestSignUpDto body)
  {
    await _authenticationService.RequestSignUp(body.Email);

    return Ok();
  }

  [HttpPost("SignUp/ConfirmVerificationCode")]
  public async Task<ActionResult> SignUpConfirmVerificationCode(
    [FromBody] SignUpConfirmVerificationCodeDto body)
  {
    var result = await _authenticationService.SignUpConfirmVerificationCode(body.Email, body.Code);

    return Ok(result);
  }

  [HttpPost("SignUp")]
  public async Task<ActionResult<AuthenticationPayloadDto>> FinishSignUp(
    [FromBody] FinishSignUpDto body)
  {
    var result = await _authenticationService.FinishSignUp(
      body.Uuid,
      body.FirstName,
      body.LastName,
      body.Gender,
      body.BirthDate,
      body.Password
    );

    return Ok(result);
  }

  [HttpPost("SignIn")]
  public async Task<ActionResult<AuthenticationPayloadDto>> SignIn([FromBody] SignInDto body)
  {
    var result = await _authenticationService.SignIn(body.Email, body.Password);

    return Ok(result);
  }

  [HttpPost("Refresh")]
  public async Task<ActionResult<AuthenticationPayloadDto>> Refresh([FromBody] RefreshTokenDto body)
  {
    var result = await _authenticationService.Refresh(body.RefreshToken);

    return Ok(result);
  }

  [HttpPost("Recover/SendVerificationCode")]
  public async Task<ActionResult> RecoverSendVerificationCode(
    [FromBody] RecoverSendVerificationCodeDto body)
  {
    await _authenticationService.RecoverSendVerificationCode(body.Email);

    return Ok();
  }

  [HttpPost("Recover/ConfirmVerificationCode")]
  public async Task<ActionResult<RecoverConfirmVerificationCodeResponseDto>>
    RecoverConfirmVerificationCode([FromBody] RecoverConfirmVerificationCodeDto body)
  {
    var result = await _authenticationService.RecoverConfirmVerificationCode(body.Email, body.Code);

    return Ok(result);
  }

  [HttpPost("Recover")]
  public async Task<ActionResult> RecoverPassword([FromBody] RecoverPasswordDto body)
  {
    await _authenticationService.RecoverPassword(body.Uuid, body.NewPassword);

    return Ok();
  }
}