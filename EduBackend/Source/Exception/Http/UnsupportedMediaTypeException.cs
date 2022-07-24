using System.Net;

namespace EduBackend.Source.Exception.Http;

public class UnsupportedMediaTypeException : GenericException
{
  public UnsupportedMediaTypeException(string message) : base(
    message,
    HttpStatusCode.UnsupportedMediaType
  )
  {
  }
}