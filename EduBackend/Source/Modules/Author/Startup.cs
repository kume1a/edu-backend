namespace EduBackend.Source.Modules.Author;

public static class Startup
{
  public static IServiceCollection AddAuthorModule(this IServiceCollection services)
  {
    services.AddScoped<IAuthorRepository, AuthorRepository>()
      .AddScoped<IAuthorService, AuthorService>();

    return services;
  }
}