using HelperGeneral.Models;
using System.Net;
using System.Net.Mail;

namespace SMTP_api.Helper.Helper
{
    public class SmtpHelper
    {
        private readonly string _from;
        private readonly string _user;
        private readonly string _pass;
        private readonly string _domain;
        private readonly int _port;
        private readonly bool _requiredSsl;


        public SmtpHelper()
        {
            IConfiguration smtpC = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("smtpConfig");
            _from = smtpC.GetValue<string>("from");
            _user = smtpC.GetValue<string>("user");
            _pass = smtpC.GetValue<string>("pass");
            _domain = smtpC.GetValue<string>("domain");
            _port = smtpC.GetValue<int>("port");
            _requiredSsl = smtpC.GetValue<bool>("requiredSsl");
        }

        public ResponseData<string> SendMail(
            string subject,
            string body,
            List<string> to,
            List<string>? cc = null,
            List<string>? bcc = null,
            List<Attachment>? listFiles = null
        )
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(_from);

                foreach (string item in to)
                {
                    message.To.Add(item);
                }

                foreach (string item in cc ?? [])
                {
                    message.CC.Add(item);
                }

                foreach (string item in bcc ?? [])
                {
                    message.Bcc.Add(item);
                }

                message.Subject = subject;
                message.Body = body;


                foreach (Attachment attachment in listFiles ?? [])
                {
                    message.Attachments.Add(attachment);
                }

                SmtpClient smtpClient = new SmtpClient(_domain)
                {
                    Port = _port,
                    Credentials = new NetworkCredential(_user, _pass),
                    EnableSsl = _requiredSsl,
                };

                smtpClient.Send(message);

                return new ResponseData<string>("");
            } catch (Exception ex)
            {
                return new ResponseData<string>("Error envio correo", ex.ToString());
            }
        }
    }
}
