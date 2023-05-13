using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using PVP_Projektas_API.Interfaces;
using PVP_Projektas_API.Repository;

namespace PVP_Projektas_API.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task SendMail(string to, string subject, string body)
        {
            MimeMessage mimeMessage = new MimeMessage();

            mimeMessage.From.Add(new MailboxAddress(_configuration.GetValue<string>("MailSettings:SenderName"), _configuration.GetValue<string>("MailSettings:SenderEmail")));
            mimeMessage.To.Add(new MailboxAddress("", to));

            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart("plain") { Text = body };

            using var client = new SmtpClient();
            client.Connect(_configuration.GetValue<string>("MailSettings:Server"), _configuration.GetValue<int>("MailSettings:Port"), SecureSocketOptions.StartTls);
            client.Authenticate(_configuration.GetValue<string>("MailSettings:UserName"), _configuration.GetValue<string>("MailSettings:Password"));

            client.Send(mimeMessage);
            client.Disconnect(true);
            return Task.CompletedTask;
        }
    }
}