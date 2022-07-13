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
  
  [HttpPost("ResendConfirmAccountEmailCode")]
  public async Task<ActionResult> ResendConfirmAccountEmailCode()
  {
    await _authenticationService.ResendConfirmAccountEmailCode(-1); // TODO fix user id

    return Ok();
  }

  [HttpPost("ConfirmAccountEmail")]
  public async Task<ActionResult> ConfirmAccountEmail(
    [FromBody] ConfirmAccountEmailDto body)
  {
    await _authenticationService.ConfirmAccountEmail(-1, body.Code); // TODO fix user id

    return Ok();
  }

  [HttpPost("SignUp")]
  public async Task<ActionResult<AuthenticationPayloadDto>> SignUp(
    [FromBody] SignUpDto body)
  {
    var result = await _authenticationService.SignUp(
      body.Email,
      body.Username,
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