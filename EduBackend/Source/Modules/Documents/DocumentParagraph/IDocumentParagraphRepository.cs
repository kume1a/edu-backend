using EduBackend.Source.Model.Common;

namespace EduBackend.Source.Modules.Documents.DocumentParagraph;

public interface IDocumentParagraphRepository
{
  Task<Model.Entity.DocumentParagraph> CreateEntity(
    long documentId,
    string title,
    int index,
    string content);

  Task<DataPage<Model.Entity.DocumentParagraph>> Filter(long documentId, int page, int pageSize);
  Task<bool> DeleteById(long id);

  Task<Model.Entity.DocumentParagraph?> UpdateById(
    long id,
    string? title,
    int? index,
    string? content);

  Task<Model.Entity.DocumentParagraph?> GetById(long id);
}