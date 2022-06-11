using EduBackend.Source.Model.Common;
using EduBackend.Source.Model.DTO.Authentication;

namespace EduBackend.Source.Modules.Authentication;

public interface IAuthenticationService
{
  public Task<AuthenticationPayloadDto> SignIn(string email, string password);

  public Task<AuthenticationPayloadDto> SignUp(string username, string email, string password);

  public Task<AuthenticationPayloadDto> Refresh(string refreshToken);
}