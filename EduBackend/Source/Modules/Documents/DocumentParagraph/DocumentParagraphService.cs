using EduBackend.Source.Exception;
using EduBackend.Source.Exception.Http;
using EduBackend.Source.Model.DTO.Common;
using EduBackend.Source.Model.DTO.Document;
using EduBackend.Source.Model.Mapper.DocumentParagraph;

namespace EduBackend.Source.Modules.Documents.DocumentParagraph;

public class DocumentParagraphService : IDocumentParagraphService
{
  private readonly IDocumentParagraphRepository _documentParagraphRepository;
  private readonly IDocumentParagraphMapper _documentParagraphMapper;
  private readonly IDocumentService _documentService;

  public DocumentParagraphService(
    IDocumentParagraphRepository documentParagraphRepository,
    IDocumentParagraphMapper documentParagraphMapper,
    IDocumentService documentService)
  {
    _documentParagraphRepository = documentParagraphRepository;
    _documentParagraphMapper = documentParagraphMapper;
    _documentService = documentService;
  }

  public async Task<DocumentParagraphDto> CreateDocumentParagraph(
    long documentId,
    string title,
    int index,
    string content)
  {
    await _documentService.ValidateDocumentById(documentId);

    var documentParagraph =
      await _documentParagraphRepository.CreateEntity(documentId, title, index, content);

    return _documentParagraphMapper.ShallowMap(documentParagraph);
  }

  public async Task<DataPageDto<DocumentParagraphDto>> GetDocumentParagraphsByDocumentId(
    long documentId,
    int page,
    int pageSize,
    string? searchQuery)
  {
    var documentParagraphs =
      await _documentParagraphRepository.Filter(documentId, page, pageSize, searchQuery);

    return DataPageDto<DocumentParagraphDto>.fromDataPage(
      documentParagraphs,
      _documentParagraphMapper.ShallowMap
    );
  }

  public async Task DeleteDocumentParagraphById(long id)
  {
    var didDelete = await _documentParagraphRepository.DeleteById(id);
    if (!didDelete)
    {
      throw new NotFoundException(ExceptionMessageCode.DocumentParagraphNotFound);
    }
  }

  public async Task<DocumentParagraphDto> UpdateDocumentParagraphById(
    long id,
    string? title,
    int? index,
    string? content)
  {
    var documentParagraph =
      await _documentParagraphRepository.UpdateById(id, title, index, content);
    if (documentParagraph is null)
    {
      throw new NotFoundException(ExceptionMessageCode.DocumentParagraphNotFound);
    }

    return _documentParagraphMapper.ShallowMap(documentParagraph);
  }

  public async Task<DocumentParagraphDto> GetDocumentParagraphById(long id)
  {
    var documentParagraph = await _documentParagraphRepository.GetById(id);
    if (documentParagraph is null)
    {
      throw new NotFoundException(ExceptionMessageCode.DocumentParagraphNotFound);
    }

    return _documentParagraphMapper.ShallowMap(documentParagraph);
  }
}