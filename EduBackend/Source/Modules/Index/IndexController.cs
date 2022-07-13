using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduBackend.Source.Modules.Index;

[ApiController]
[Route("")]
[AllowAnonymous]
public class IndexController : ControllerBase
{
  [HttpGet]
  public async Task<ContentResult> Index()
  {
    var html = await System.IO.File.ReadAllTextAsync(@"./Source/Modules/Index/index.html");
    
    return new ContentResult
    {
      ContentType = "text/html",
      StatusCode = 200,
      Content = html
    };
  }
}