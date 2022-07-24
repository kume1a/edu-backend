using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace EduBackend.Source.Common.Helper;

public class EmailClient : IEmailClient
{
  private readonly IConfiguration _config;

  public EmailClient(IConfiguration config)
  {
    _config = config;
  }

  public async Task SendEmailAsync(string toEmail, string subject, string message)
  {
    var mailMessage = new MimeMessage();
    mailMessage.From.Add(new MailboxAddress("FROM_NAME", _config["Smtp:Username"]));
    mailMessage.To.Add(new MailboxAddress(toEmail, toEmail));
    mailMessage.Subject = subject;
    mailMessage.Body = new TextPart("plain") { Text = message };

    using var smtpClient = new SmtpClient();

    await smtpClient.ConnectAsync(
      _config["Smtp:Host"],
      int.Parse(_config["Smtp:Port"]),
      SecureSocketOptions.StartTlsWhenAvailable
    );
    await smtpClient.AuthenticateAsync(_config["Smtp:Username"], _config["Smtp:Password"]);
    await smtpClient.SendAsync(mailMessage);
    await smtpClient.DisconnectAsync(true);
  }
}