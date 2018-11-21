using System.Net;
using System.Net.Mail;
using Contexts;
using Microsoft.Extensions.Options;
using Models;
using Models.DB;

namespace Services
{
    public class MainServcie
    {
        private readonly MainContext __context;
        private readonly EmailSettings __EmailSettings;
        public MainServcie(MainContext context, IOptions<EmailSettings> settings, IOptions<Urls> urlSettings)
        {
            this.__context = context;
            this.__EmailSettings = settings.Value;
        }
        public async void SendEmail(string subject, string body, bool isBodyHtml, string email)
        {
            var test = this.__EmailSettings.Host;
            var smtpClient = new SmtpClient
            {
                Host = this.__EmailSettings.Host, // set your SMTP server name here
                Port = 587, // Port 
                EnableSsl = true,
                Credentials = new NetworkCredential(this.__EmailSettings.Email, this.__EmailSettings.Password)
            };
            using (var message = new MailMessage(this.__EmailSettings.Email, email)
            {
                IsBodyHtml = isBodyHtml,
                Subject = subject,
                Body = body,
            })
            {
                await smtpClient.SendMailAsync(message);
            }
        }
    }
}