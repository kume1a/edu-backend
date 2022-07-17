using EduBackend.Source.Model.DTO.Common;
using EduBackend.Source.Model.DTO.Document;
using EduBackend.Source.Model.Enum;
using EduBackend.Source.Modules.Documents.DocumentParagraph;
using Microsoft.AspNetCore.Mvc;

namespace EduBackend.Source.Modules.Documents;

[ApiController]
[Route("[controller]")]
public class DocumentController : ControllerBase
{
  private readonly IDocumentService _documentService;
  private readonly IDocumentParagraphService _documentParagraphService;

  public DocumentController(
    IDocumentService documentService,
    IDocumentParagraphService documentParagraphService)
  {
    _documentService = documentService;
    _documentParagraphService = documentParagraphService;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<DocumentDto>>> GetDocuments()
  {
    var documents = await _documentService.GetAllDocuments();

    return Ok(documents);
  }

  [HttpGet("PrivacyPolicy")]
  public async Task<ActionResult<DocumentDto>> GetPrivacyPolicy()
  {
    var document = await _documentService.GetDocumentByDocumentType(DocumentType.PrivacyPolicy);

    return Ok(document);
  }

  [HttpGet("TermsOfService")]
  public async Task<ActionResult<DocumentDto>> GetTermsOfService()
  {
    var document = await _documentService.GetDocumentByDocumentType(DocumentType.TermsOfService);

    return Ok(document);
  }

  [HttpGet("Paragraphs/{id}")]
  public async Task<ActionResult<DocumentParagraphDto>> GetDocumentParagraph([FromRoute] long id)
  {
    var documentParagraph = await _documentParagraphService.GetDocumentParagraphById(id);

    return Ok(documentParagraph);
  }

  [HttpPatch("Paragraphs/{id}")]
  public async Task<ActionResult<DocumentParagraphDto>> UpdateDocumentParagraph(
    [FromRoute] long id,
    [FromBody] UpdateDocumentParagraphDto body)
  {
    var documentParagraph = await _documentParagraphService.UpdateDocumentParagraphById(
      id,
      body.Title,
      body.Index,
      body.Content
    );

    return Ok(documentParagraph);
  }

  [HttpDelete("Paragraphs/{id}")]
  public async Task<ActionResult> DeleteDocumentParagraph([FromRoute] long id)
  {
    await _documentParagraphService.DeleteDocumentParagraphById(id);

    return Ok();
  }

  [HttpGet("{documentId}/Paragraphs")]
  public async Task<ActionResult<DataPageDto<DocumentParagraphDto>>> GetDocumentParagraphs(
    [FromRoute] long documentId,
    [FromQuery] FilterDocumentParagraphsDto query)
  {
    var documentParagraphs =
      await _documentParagraphService.GetDocumentParagraphsByDocumentId(
        documentId,
        query.Page,
        query.PageSize
      );

    return Ok(documentParagraphs);
  }

  [HttpPost("{documentId}/Paragraphs")]
  public async Task<ActionResult<DocumentParagraphDto>> CreateDocumentParagraph(
    [FromRoute] long documentId,
    [FromBody] CreateDocumentParagraphDto body)
  {
    var documentParagraph = await _documentParagraphService.CreateDocumentParagraph(
      documentId,
      body.Title,
      body.Index,
      body.Content
    );

    return Created(documentParagraph.Id.ToString(), documentParagraph);
  }
}