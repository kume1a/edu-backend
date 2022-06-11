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

  [HttpPost("SignUp")]
  public async Task<ActionResult<AuthenticationPayloadDto>> SignUp([FromBody] SignUpDto body)
  {
    var result = await _authenticationService.SignUp(body.Username, body.Email, body.Password);

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
}