using System.Net;

namespace EduBackend.Source.Exception.Http;

public class UnauthorizedException : GenericException
{
  public UnauthorizedException(string message) : base(message, HttpStatusCode.Unauthorized)
  {
  }
}