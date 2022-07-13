using EduBackend.Source.Model.DTO.Authentication;
using EduBackend.Source.Model.Enum;

namespace EduBackend.Source.Modules.Authentication;

public interface IAuthenticationService
{
  Task<AuthenticationPayloadDto> SignIn(string email, string password);

  Task<AuthenticationPayloadDto> Refresh(string refreshToken);

  Task RecoverSendVerificationCode(string email);

  Task<RecoverConfirmVerificationCodeResponseDto> RecoverConfirmVerificationCode(
    string email,
    string code);

  Task RecoverPassword(string uuid, string newPassword);

  Task ConfirmAccountEmail(long userId, string code);

  Task<AuthenticationPayloadDto> SignUp(
    string email,
    string username,
    Gender gender,
    DateTime birthDate,
    string password);

  Task ResendConfirmAccountEmailCode(long userId);
}