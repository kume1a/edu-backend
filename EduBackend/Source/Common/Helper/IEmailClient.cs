namespace EduBackend.Source.Common.Helper;

public interface IEmailClient
{
  Task SendEmailAsync(string toEmail, string subject, string message);
}