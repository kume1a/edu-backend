namespace EduBackend.Source.Common;

public interface IEmailClient
{
  Task SendEmailAsync(string toEmail, string subject, string message);
}