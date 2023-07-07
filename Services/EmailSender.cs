using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DocDocGo.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;
        private readonly string _smtpServer = "new server";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUsername= "Sami";
        private readonly string _smtpPassword = "Password123-_";
        public EmailSender(ILogger<EmailSender> logger)
        {
            _logger = logger;
            // Use dummy values for testing
            _smtpServer = "dummy-smtp-server";
            _smtpPort = 587;
            _smtpUsername = "dummy-username";
            _smtpPassword = "dummy-password";
        }
        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            try
            {
                using (var client = new SmtpClient(_smtpServer, _smtpPort))
                {
                    client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_smtpUsername),
                        Subject = subject,
                        Body = message,
                        IsBodyHtml = true
                    };
                    mailMessage.To.Add(toEmail);

                    await client.SendMailAsync(mailMessage);

                    _logger.LogInformation($"Email to {toEmail} sent successfully!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send email to {toEmail}. Error: {ex.Message}");
                throw;
            }
        }
    }
}
