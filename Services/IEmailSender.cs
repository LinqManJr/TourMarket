using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using TourMarket.Helpers;

namespace TourMarket.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string from, string to, string message);
    }

    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _configuration;

        public EmailSender(EmailConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmailAsync(string from, string to, string text)
        {
            return Task.Factory.StartNew(() =>
            {
                var message = new MimeMessage();
                
                message.To.Add(new MailboxAddress(to));
                message.From.Add(new MailboxAddress(from));
                message.Subject = "Notification from tour market";

                message.Body = new TextPart(TextFormat.Html)
                {
                    Text = text
                };

                using (var emailClient = new SmtpClient())
                {
                    emailClient.Connect(_configuration.SmtpServer, _configuration.Port, true);

                    emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                    
                    emailClient.Authenticate(_configuration.Username, _configuration.Password);

                    emailClient.Send(message);

                    emailClient.Disconnect(true);
                }
            });
        }
    }
}
