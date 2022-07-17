using EduBackend.Source.Model.DTO.Document;
using EduBackend.Source.Model.Mapper.DocumentParagraph;

namespace EduBackend.Source.Model.Mapper.Document;

public class DocumentMapper: IDocumentMapper
{
  private readonly IDocumentParagraphMapper _documentParagraphMapper;

  public DocumentMapper(IDocumentParagraphMapper documentParagraphMapper)
  {
    _documentParagraphMapper = documentParagraphMapper;
  }
  
  public DocumentDto DeepMap(Entity.Document document)
  {
    return new DocumentDto
    {
      Id = document.Id,
      Title = document.Title,
      CreatedAt = document.CreatedAt,
      UpdatedAt = document.UpdatedAt,
      Paragraphs = document.Paragraphs.Select(_documentParagraphMapper.ShallowMap)
    };
  }

  public DocumentDto ShallowMap(Entity.Document document)
  {
    return new DocumentDto
    {
      Id = document.Id,
      Title = document.Title,
      CreatedAt = document.CreatedAt,
      UpdatedAt = document.UpdatedAt,
    };
  }
}