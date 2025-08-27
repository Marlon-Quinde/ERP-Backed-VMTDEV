using ERP.Bll.Security.Authentication;
using ERP.Helper.Data;
using ERP.Helper.Models;
using ERP.Models.Security.Authentication;
using ERP.Validate.Security;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP.Controllers.Security
{
    [Route("api/Security/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        IAuthenticationBll bll;

        public AuthenticationController(IAuthenticationBll bll)
        {
            this.bll = bll;
        }

        [HttpPost("Login")]
        public ResponseGeneralModel<LoginResponseModel?> Login(LoginRequestModel requestModel)
        {
            try
            {
                return bll.Login(requestModel);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<LoginResponseModel?>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }

        [HttpPost("Register")]
        public ResponseGeneralModel<bool> Register(RegisterRequestModel requestModel)
        {
            try
            {
                ResponseGeneralModel<bool> validate = (new AuthenticationValidate()).Register(requestModel);
                if (validate.code != 200) return validate;

                return bll.Register(requestModel);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<bool>(500, false, MessageHelper.errorGeneral, ex.ToString());
            }
        }
    }
}