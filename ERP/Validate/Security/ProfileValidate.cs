
using Ejercicio_estructurado.Helpers.Vars;
using ERP.Helper.Data;
using ERP.Helper.Models;
using ERP.Models.Security.Profile;
using HelperGeneral.Helper;
using HelperGeneral.Models;

namespace ERP.Validate.Security
{
    public class ProfileValidate
    {
        public ResponseData<string?> ChangePasswordUser(ChangePasswordRequestModel model)
        {
            ValidateHelper<string?> validaH = new ValidateHelper<string?>();

            ResponseData<string?> valOldPass = validaH.ValidResp(model.oldPassword, "oldPassword", ListRegExp: new List<string>() { VarHelper.regExParamString });
            if (valOldPass.isTrue) return valOldPass;
            ResponseData<string?> valNewPass = validaH.ValidResp(model.newPassword, "newPassword", ListRegExp: new List<string>() { VarHelper.regExParamString });
            if (valNewPass.isTrue) return valNewPass;
            ResponseData<string?> valRepNewPass = validaH.ValidResp(model.repeatNewPassword, "repeatNewPassword", ListRegExp: new List<string>() { VarHelper.regExParamString });
            if (valRepNewPass.isTrue) return valRepNewPass;

            if (model.newPassword != model.repeatNewPassword) return new ResponseData<string?>(MessageHelper.profileChangePasswordErrorNotEqualsPass, MessageHelper.profileChangePasswordErrorNotEqualsPass);

            return new ResponseData<string?>();
        }

        public ResponseData<string?> ChangeNameUser(ChangeNameUserRequestModel model)
        {
            ValidateHelper<string?> validaH = new ValidateHelper<string?>();

            ResponseData<string?> nameUser = validaH.ValidResp(model.userName, "userName", ListRegExp: new List<string>() { VarHelper.regExParamString });
            if (nameUser.isTrue) return nameUser;

            return new ResponseData<string?>();
        }
    }
}
