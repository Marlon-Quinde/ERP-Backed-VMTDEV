namespace SMTP_api.Models.Mail
{
    public class SendMailRequest
    {
        public string to {  get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public List<SendMailRequest_File>? files { get; set; }
    }

    public class SendMailRequest_File
    {
        public string name { get; set; }
        public string typeFile { get; set; }
        public string base64 { get; set; }
    }
}
