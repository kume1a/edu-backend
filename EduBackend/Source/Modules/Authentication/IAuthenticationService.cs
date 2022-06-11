using EduBackend.Source.Model.Common;
using EduBackend.Source.Model.DTO.Authentication;

namespace EduBackend.Source.Modules.Authentication;

public interface IAuthenticationService
{
  Task<AuthenticationPayloadDto> SignIn(string email, string password);

  Task<AuthenticationPayloadDto> SignUp(string username, string email, string password);

  Task<AuthenticationPayloadDto> Refresh(string refreshToken);
}