using EduBackend.Source.Model.Entity;
using EduBackend.Source.Model.Enum;

namespace EduBackend.Source.Modules.Documents;

public interface IDocumentRepository
{
  Task<Document?> GetByDocumentType(DocumentType documentType);
  Task<IEnumerable<Document>> GetAll();
}