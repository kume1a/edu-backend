using EduBackend.Source.Model.DTO.Document;

namespace EduBackend.Source.Model.Mapper.Document;

public interface IDocumentMapper
{
  DocumentDto DeepMap(Entity.Document document);
  DocumentDto ShallowMap(Entity.Document document);
}