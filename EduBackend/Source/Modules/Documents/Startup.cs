using EduBackend.Source.Modules.Documents.DocumentParagraph;

namespace EduBackend.Source.Modules.Documents;

public static class Startup
{
  public static IServiceCollection AddDocumentModule(this IServiceCollection services)
  {
    services
      .AddScoped<IDocumentParagraphRepository, DocumentParagraphRepository>()
      .AddScoped<IDocumentRepository, DocumentRepository>()
      .AddScoped<IDocumentParagraphService, DocumentParagraphService>()
      .AddScoped<IDocumentService, DocumentService>();

    return services;
  }
}