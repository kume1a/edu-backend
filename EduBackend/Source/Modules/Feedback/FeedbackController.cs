using EduBackend.Source.Common;
using EduBackend.Source.Model.DTO.Common;
using EduBackend.Source.Model.DTO.Feedback;
using Microsoft.AspNetCore.Mvc;

namespace EduBackend.Source.Modules.Feedback;

[ApiController]
[Route("[controller]")]
public class FeedbackController : ControllerBase
{
  private readonly IFeedbackService _feedbackService;

  public FeedbackController(IFeedbackService feedbackService)
  {
    _feedbackService = feedbackService;
  }

  [HttpPost]
  public async Task<ActionResult<FeedbackDto>> CreateFeedback([FromBody] CreateFeedbackDto body)
  {
    var feedback = await _feedbackService.CreateFeedback(
      User.GetUserId(),
      body.Content,
      body.Review
    );

    return Created(feedback.Id.ToString(), feedback);
  }
  
  [HttpGet]
  public async Task<ActionResult<DataPageDto<FeedbackDto>>> FilterFeedback([FromQuery] FilterFeedbackDto query)
  {
    var page = await _feedbackService.FilterFeedback(
      query.Page,
      query.PageSize,
      query.SearchQuery
    );

    return Ok(page);
  }
}