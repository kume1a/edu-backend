using EduBackend.Source.Model;
using EduBackend.Source.Model.Entity;
using EduBackend.Source.Model.Enum;
using Microsoft.EntityFrameworkCore;

namespace EduBackend.Source.Modules.Documents;

public class DocumentRepository : IDocumentRepository
{
  private readonly DataContext _db;

  public DocumentRepository(DataContext db)
  {
    _db = db;
  }

  public async Task<Document?> GetByDocumentType(DocumentType documentType)
  {
    return await _db.Documents
      .AsNoTracking()
      .Include(document => document.Paragraphs)
      .SingleOrDefaultAsync(document => document.DocumentType == documentType);
  }

  public async Task<IEnumerable<Document>> GetAll()
  {
    return await _db.Documents.AsNoTracking().ToListAsync();
  }
}