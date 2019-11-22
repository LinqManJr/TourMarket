using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit;
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

        public Task SendEmailAsync(string from, string to, string message)
        {
            throw new NotImplementedException();
        }
    }
}
