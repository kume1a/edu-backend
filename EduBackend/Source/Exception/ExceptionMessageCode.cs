namespace EduBackend.Source.Exception;

public static class ExceptionMessageCode
{
  public static readonly string InternalServerException = nameof(InternalServerException);
  public static readonly string PermissionDenied = nameof(PermissionDenied);
  public static readonly string InvalidEmailOrPassword = nameof(InvalidEmailOrPassword);
  public static readonly string EmailAlreadyInUse = nameof(EmailAlreadyInUse);
  public static readonly string UsernameAlreadyInUse = nameof(UsernameAlreadyInUse);
  public static readonly string InvalidToken = nameof(InvalidToken);
}