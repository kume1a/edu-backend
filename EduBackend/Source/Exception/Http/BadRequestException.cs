using System.Net;

namespace EduBackend.Source.Exception.Http;

public class BadRequestException : GenericException
{
  public BadRequestException(string message) : base(message, HttpStatusCode.BadRequest)
  {
  }
}