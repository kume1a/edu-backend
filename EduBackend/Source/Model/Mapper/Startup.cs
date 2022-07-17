using EduBackend.Source.Model.Mapper.Document;
using EduBackend.Source.Model.Mapper.DocumentParagraph;
using EduBackend.Source.Model.Mapper.Role;

namespace EduBackend.Source.Model.Mapper;

public static class Startup
{
  public static IServiceCollection AddMapperModule(this IServiceCollection services)
  {
    services.AddScoped<IRoleMapper, RoleMapper>()
      .AddScoped<IRoleClaimMapper, RoleClaimMapper>()
      .AddScoped<IDocumentParagraphMapper, DocumentParagraphMapper>()
      .AddScoped<IDocumentMapper, DocumentMapper>();

    return services;
  }
}