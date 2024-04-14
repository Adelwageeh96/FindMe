

namespace FindMe.Infrastructure.Services.Mail
{
    public class MailSettings
    {
        public const String Section = "MailSettings";
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host {  get; set; }
        public int Port { get; set; }
    }
}
