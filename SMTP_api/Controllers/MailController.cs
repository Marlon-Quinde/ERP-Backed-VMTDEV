using HelperGeneral.Models;
using Microsoft.AspNetCore.Mvc;
using SMTP_api.Helper.Helper;
using SMTP_api.Models.Mail;
using SMTP_api.Validate;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SMTP_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {

        // POST api/<MailController>
        [HttpPost("Send")]
        public ResponseData<string?> Send([FromBody] SendMailRequest requestModel)
        {
            MailValidate mailV = new MailValidate();
            ResponseData<string?> respV = mailV.SendMail(requestModel);
            if(respV.isTrue != true) return respV;

            return (new SmtpHelper()).SendMail(requestModel.subject, requestModel.body, [requestModel.to], listFiles: requestModel.files);
        }
    }
}
