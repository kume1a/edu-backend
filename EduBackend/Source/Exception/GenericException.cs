using System.Net;

namespace EduBackend.Source.Exception;

public class GenericException : System.Exception
{
  public HttpStatusCode StatusCode { get; }

  public GenericException(string message, HttpStatusCode statusCode) : base(message)
  {
    StatusCode = statusCode;
  }
}