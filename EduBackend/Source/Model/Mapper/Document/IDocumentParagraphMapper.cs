using EduBackend.Source.Model.DTO.Document;

namespace EduBackend.Source.Model.Mapper.DocumentParagraph;

public interface IDocumentParagraphMapper
{
  DocumentParagraphDto ShallowMap(Entity.DocumentParagraph documentParagraph);
}