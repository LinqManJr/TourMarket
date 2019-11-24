using Newtonsoft.Json;

namespace TourMarket.Helpers
{
    public class EmailConfiguration
    {
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
                
        /*public EmailConfiguration(string server, int port, string user, string pass)
        {
            SmtpServer = server;
            Port = port;
            Username = user;
            Password = pass;
        }*/
    }
}
