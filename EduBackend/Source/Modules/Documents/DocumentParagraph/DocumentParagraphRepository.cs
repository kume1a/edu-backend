using EduBackend.Source.Model;
using EduBackend.Source.Model.Common;
using Microsoft.EntityFrameworkCore;

namespace EduBackend.Source.Modules.Documents.DocumentParagraph;

public class DocumentParagraphRepository : IDocumentParagraphRepository
{
  private readonly DataContext _db;

  public DocumentParagraphRepository(DataContext db)
  {
    _db = db;
  }

  public async Task<Model.Entity.DocumentParagraph> CreateEntity(
    long documentId,
    string title,
    int index,
    string content)
  {
    var entity = new Model.Entity.DocumentParagraph
    {
      DocumentId = documentId,
      Title = title,
      Index = index,
      Content = content
    };

    await _db.DocumentParagraphs.AddAsync(entity);
    await _db.SaveChangesAsync();

    return entity;
  }

  public async Task<DataPage<Model.Entity.DocumentParagraph>> Filter(
    long documentId,
    int page,
    int pageSize)
  {
    var query =
      _db.DocumentParagraphs
        .Where(documentParagraph => documentParagraph.DocumentId == documentId)
        .OrderByDescending(documentParagraph => documentParagraph.CreatedAt)
        .AsNoTracking();

    return await DataPage<Model.Entity.DocumentParagraph>.FromQuery(query, page, pageSize);
  }

  public async Task<bool> DeleteById(long id)
  {
    var entity =
      await _db.DocumentParagraphs.SingleOrDefaultAsync(
        documentParagraph => documentParagraph.Id == id
      );
    if (entity is null)
    {
      return false;
    }

    _db.DocumentParagraphs.Remove(entity);
    await _db.SaveChangesAsync();

    return true;
  }

  public async Task<Model.Entity.DocumentParagraph?> UpdateById(
    long id,
    string? title,
    int? index,
    string? content)
  {
    var entity = await _db.DocumentParagraphs.SingleOrDefaultAsync(
      documentParagraph => documentParagraph.Id == id
    );
    if (entity is null)
    {
      return null;
    }

    if (title is not null) entity.Title = title;
    if (index is not null) entity.Index = index.Value;
    if (content is not null) entity.Content = content;

    _db.DocumentParagraphs.Update(entity);
    await _db.SaveChangesAsync();

    return entity;
  }

  public Task<Model.Entity.DocumentParagraph?> GetById(long id)
  {
    return _db.DocumentParagraphs
      .AsNoTracking()
      .SingleOrDefaultAsync(documentParagraph => documentParagraph.Id == id);
  }
}