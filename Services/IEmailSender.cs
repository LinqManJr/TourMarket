using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit;

namespace TourMarket.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string from, string to, string message);
    }

    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string from, string to, string message)
        {
            throw new NotImplementedException();
        }
    }
}
