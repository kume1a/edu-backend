using EduBackend.Source.Exception;
using EduBackend.Source.Exception.Http;
using EduBackend.Source.Model.DTO.Document;
using EduBackend.Source.Model.Enum;
using EduBackend.Source.Model.Mapper.Document;

namespace EduBackend.Source.Modules.Documents;

public class DocumentService: IDocumentService
{
  private readonly IDocumentRepository _documentRepository;
  private readonly IDocumentMapper _documentMapper;

  public DocumentService(IDocumentRepository documentRepository, IDocumentMapper documentMapper)
  {
    _documentRepository = documentRepository;
    _documentMapper = documentMapper;
  }
  
  public async Task<DocumentDto> GetDocumentByDocumentType(DocumentType documentType)
  {
    var document = await _documentRepository.GetByDocumentType(documentType);
    if (document is null)
    {
      throw new NotFoundException(ExceptionMessageCode.DocumentNotFound);
    }

    return _documentMapper.DeepMap(document);
  }

  public async Task<IEnumerable<DocumentDto>> GetAllDocuments()
  {
    var documents = await _documentRepository.GetAll();

    return documents.Select(_documentMapper.ShallowMap);
  }

  public async Task ValidateDocumentById(long documentId)
  {
    if (!await _documentRepository.ExistsById(documentId))
    {
      throw new NotFoundException(ExceptionMessageCode.DocumentNotFound);
    }
  }

  public async Task<DocumentDto> GetDocumentById(long documentId)
  {
    var document = await _documentRepository.GetById(documentId);
    if (document is null)
    {
      throw new NotFoundException(ExceptionMessageCode.DocumentNotFound);
    }

    return _documentMapper.ShallowMap(document);
  }
}