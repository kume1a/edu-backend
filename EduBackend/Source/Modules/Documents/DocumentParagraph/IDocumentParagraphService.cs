using EduBackend.Source.Model.DTO.Common;
using EduBackend.Source.Model.DTO.Document;

namespace EduBackend.Source.Modules.Documents.DocumentParagraph;

public interface IDocumentParagraphService
{
  Task<DocumentParagraphDto> CreateDocumentParagraph(
    long documentId,
    string title,
    int index,
    string content);

  Task<DataPageDto<DocumentParagraphDto>> GetDocumentParagraphsByDocumentId(
    long documentId,
    int page,
    int pageSize,
    string? searchQuery);
  Task DeleteDocumentParagraphById(long id);

  Task<DocumentParagraphDto> UpdateDocumentParagraphById(
    long id,
    string? title,
    int? index,
    string? content);

  Task<DocumentParagraphDto> GetDocumentParagraphById(long id);
}