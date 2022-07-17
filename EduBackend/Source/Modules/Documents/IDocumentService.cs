using EduBackend.Source.Model.DTO.Document;
using EduBackend.Source.Model.Enum;

namespace EduBackend.Source.Modules.Documents;

public interface IDocumentService
{
  Task<DocumentDto> GetDocumentByDocumentType(DocumentType documentType);
  Task<IEnumerable<DocumentDto>> GetAllDocuments();
}