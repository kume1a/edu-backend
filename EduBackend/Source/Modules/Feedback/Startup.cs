namespace EduBackend.Source.Modules.Feedback;

public static class Startup
{
  public static IServiceCollection AddFeedbackModule(this IServiceCollection services)
  {
    services.AddScoped<IFeedbackRepository, FeedbackRepository>()
      .AddScoped<IFeedbackService, FeedbackService>();

    return services;
  }
}