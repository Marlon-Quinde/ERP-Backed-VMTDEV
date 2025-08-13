using ERP.Bll.Security.Profile;
using ERP.Filters;
using ERP.Helper.Data;
using ERP.Helper.Models;
using ERP.Models.Security.Authentication;
using ERP.Models.Security.Profile;
using ERP.Validate.Security;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP.Controllers.Security
{
    [ServiceFilter(typeof(SessionUserFilter))]
    [Route("api/Security/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        IProfileBll bll;

        public ProfileController(IProfileBll bll)
        {
            this.bll = bll;
        }

        [HttpGet("Me")]
        public ResponseGeneralModel<ProfileResponseModel> GetProfile()
        {
            try
            {
                return bll.Me();
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<ProfileResponseModel>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }

        [ServiceFilter(typeof(SessionUserFilter))]
        [HttpPut("ChangePassword")]
        public ResponseGeneralModel<string?> ChangePasswordUser(ChangePasswordRequestModel requestModel)
        {
            try
            {
                ProfileValidate profileV = new ProfileValidate();
                ResponseGeneralModel<string?> respVal = profileV.ChangePasswordUser(requestModel);
                if (respVal.code != 200) return respVal;

                return bll.ChangePasswordUser(requestModel);
            } catch (Exception ex)
            {
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }


        [HttpPut("Username")]
        public ResponseGeneralModel<string?> ChangeNameUSer(ChangeNameUserRequestModel requestModel)
        {
            try
            {
                ProfileValidate profileV = new ProfileValidate();
                ResponseGeneralModel<string?> respVal = profileV.ChangeNameUser(requestModel);
                if (respVal.code != 200) return respVal;

                return bll.ChangeNameUser(requestModel);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }
    }
}
