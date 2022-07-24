namespace EduBackend.Source.Exception;

public static class ExceptionMessageCode
{
  public const string InternalServerException = nameof(InternalServerException);
  public const string PermissionDenied = nameof(PermissionDenied);
  public const string InvalidEmailOrPassword = nameof(InvalidEmailOrPassword);
  public const string EmailAlreadyInUse = nameof(EmailAlreadyInUse);
  public const string InvalidToken = nameof(InvalidToken);
  public const string RefreshTokenReuse = nameof(RefreshTokenReuse);
  public const string InvalidPermission = nameof(InvalidPermission);
  public const string RoleNameAlreadyExists = nameof(RoleNameAlreadyExists);
  public const string RoleNotFound = nameof(RoleNotFound);
  public const string RecoverPasswordRequestNotFound = nameof(RecoverPasswordRequestNotFound);
  public const string RecoverPasswordRequestTimedOut = nameof(RecoverPasswordRequestTimedOut);
  public const string InvalidVerificationCode = nameof(InvalidVerificationCode);
  public const string RequestNotVerified = nameof(RequestNotVerified);
  public const string UserNotFound = nameof(UserNotFound);
  public const string AccountVerificationCodeNotFound = nameof(AccountVerificationCodeNotFound);
  public const string DocumentNotFound = nameof(DocumentNotFound);
  public const string DocumentParagraphNotFound = nameof(DocumentParagraphNotFound);
  public const string GenreNotFound = nameof(GenreNotFound);
  public const string InvalidImage = nameof(InvalidImage);
}