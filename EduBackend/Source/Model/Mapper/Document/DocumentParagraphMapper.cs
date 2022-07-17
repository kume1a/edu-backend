using EduBackend.Source.Model.DTO.Document;

namespace EduBackend.Source.Model.Mapper.DocumentParagraph;

public class DocumentParagraphMapper: IDocumentParagraphMapper
{
  public DocumentParagraphDto ShallowMap(Entity.DocumentParagraph documentParagraph)
  {
    return new DocumentParagraphDto
    {
      Id = documentParagraph.Id,
      Title = documentParagraph.Title,
      Index = documentParagraph.Index,
      Content = documentParagraph.Content,
      CreatedAt = documentParagraph.CreatedAt,
      UpdatedAt = documentParagraph.UpdatedAt,
      DocumentId = documentParagraph.DocumentId,
    };
  }
}