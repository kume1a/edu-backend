namespace EduBackend.Source.Exception;

public static class ExceptionMessageCode
{
  public const string InternalServerException = nameof(InternalServerException);
  public const string PermissionDenied = nameof(PermissionDenied);
  public const string InvalidEmailOrPassword = nameof(InvalidEmailOrPassword);
  public const string EmailAlreadyInUse = nameof(EmailAlreadyInUse);
  public const string UsernameAlreadyInUse = nameof(UsernameAlreadyInUse);
  public const string InvalidToken = nameof(InvalidToken);
  public const string RefreshTokenReuse = nameof(RefreshTokenReuse);
  public const string InvalidPermission = nameof(InvalidPermission);
  public const string RoleNameAlreadyExists = nameof(RoleNameAlreadyExists);
  public const string RoleNotFound = nameof(RoleNotFound);
}