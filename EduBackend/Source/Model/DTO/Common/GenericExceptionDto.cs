using System.Net;

namespace EduBackend.Source.Model.DTO.Common;

public class GenericExceptionDto
{
  public string Message { get; set; }

  public HttpStatusCode StatusCode { get; set; }
}