using System.Net;

namespace EduBackend.Source.Exception.Http;

public class NotFoundException : GenericException
{
  public NotFoundException(string message) : base(message, HttpStatusCode.NotFound)
  {
  }
}