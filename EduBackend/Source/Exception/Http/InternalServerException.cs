using System.Net;

namespace EduBackend.Source.Exception.Http;

public class InternalServerException : GenericException
{
  public InternalServerException() : base(
    ExceptionMessageCode.InternalServerException,
    HttpStatusCode.InternalServerError
  )
  {
  }
}