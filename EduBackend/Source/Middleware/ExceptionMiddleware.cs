using EduBackend.Source.Exception;
using EduBackend.Source.Exception.Http;
using EduBackend.Source.Model.DTO.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EduBackend.Source.Middleware;

internal class ExceptionMiddleware : IMiddleware
{
  private readonly ILogger<ExceptionMiddleware> _logger;

  public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
  {
    _logger = logger;
  }

  public async Task InvokeAsync(HttpContext context, RequestDelegate next)
  {
    try { await next(context); }
    catch (System.Exception exception)
    {
      if (exception is not GenericException && exception.InnerException != null)
      {
        while (exception.InnerException != null) { exception = exception.InnerException; }
      }

      _logger.LogInformation("_____________________{Message}_____________________\n{Trace}", exception.Message, exception.StackTrace);
      var response = context.Response;
      if (!response.HasStarted)
      {
        var resolvedException = exception as GenericException ?? new InternalServerException();
        var responseDto = new GenericExceptionDto
        {
          Message = resolvedException.Message,
          StatusCode = resolvedException.StatusCode
        };

        response.ContentType = "application/json";
        response.StatusCode = (int)resolvedException.StatusCode;

        await response.WriteAsync(
          JsonConvert.SerializeObject(
            responseDto,
            new JsonSerializerSettings
              { ContractResolver = new CamelCasePropertyNamesContractResolver() }
          )
        );
      }
      else { _logger.LogWarning("Can't write error response. Response has already started"); }
    }
  }
}