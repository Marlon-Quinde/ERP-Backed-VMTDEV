using HelperGeneral.Data;
using HelperGeneral.Helper;
using HelperGeneral.Models;
using SMTP_api.Models.Mail;

namespace SMTP_api.Validate
{
    public class MailValidate
    {
        public ResponseData<string?> SendMail(SendMailRequest model)
        {
            ValidateHelper<string?> validaH = new ValidateHelper<string?>();

            ResponseData<string?> nameUser = validaH.ValidResp(model.subject, "subject", Max: 50, Min: 8);
            if (!nameUser.isTrue) return nameUser;

            return new ResponseData<string?>();
        }
    }
}
