namespace Plastiline.PostOffice.Configuration
{
    public class SmtpConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool RequiresAuthentication { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
    }
}
