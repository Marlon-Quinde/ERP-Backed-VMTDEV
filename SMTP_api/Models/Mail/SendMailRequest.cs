namespace SMTP_api.Models.Mail
{
    public class SendMailRequest
    {
        public string to {  get; set; }
        public string subject { get; set; }
        public string body { get; set; }
    }
}
