using System.Net;

namespace EduBackend.Source.Exception.Http;

public class ForbiddenException : GenericException
{
  public ForbiddenException(string message) : base(message, HttpStatusCode.Forbidden)
  {
  }
}