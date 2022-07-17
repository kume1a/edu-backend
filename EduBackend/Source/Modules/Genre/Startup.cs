namespace EduBackend.Source.Modules.Genre;

public static class Startup
{
  public static IServiceCollection AddGenreModule(this IServiceCollection services)
  {
    services.AddScoped<IGenreRepository, GenreRepository>()
      .AddScoped<IGenreService, GenreService>();
    
    return services;
  }
}