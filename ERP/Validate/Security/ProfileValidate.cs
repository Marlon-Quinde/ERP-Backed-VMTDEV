using Ejercicio_estructurado.Helpers.Helper;
using Ejercicio_estructurado.Helpers.Vars;
using ERP.Helper.Data;
using ERP.Helper.Models;
using ERP.Models.Security.Profile;

namespace ERP.Validate.Security
{
    public class ProfileValidate
    {
        public ResponseGeneralModel<string?> ChangePasswordUser(ChangePasswordRequestModel model)
        {
            ValidateHelper<string?> validaH = new ValidateHelper<string?>();

            ResponseGeneralModel<string?> valOldPass = validaH.ValidResp(model.oldPassword, "oldPassword", ListRegExp: new List<string>() { VarHelper.regExParamString });
            if (valOldPass.code != 200) return valOldPass;
            ResponseGeneralModel<string?> valNewPass = validaH.ValidResp(model.newPassword, "newPassword", ListRegExp: new List<string>() { VarHelper.regExParamString });
            if (valNewPass.code != 200) return valNewPass;
            ResponseGeneralModel<string?> valRepNewPass = validaH.ValidResp(model.repeatNewPassword, "repeatNewPassword", ListRegExp: new List<string>() { VarHelper.regExParamString });
            if (valRepNewPass.code != 200) return valRepNewPass;

            if (model.newPassword != model.repeatNewPassword) return new ResponseGeneralModel<string?>(MessageHelper.profileChangePasswordErrorNotEqualsPass, MessageHelper.profileChangePasswordErrorNotEqualsPass, 400);

            return new ResponseGeneralModel<string?>(200, "");
        }

        public ResponseGeneralModel<string?> ChangeNameUser(ChangeNameUserRequestModel model)
        {
            ValidateHelper<string?> validaH = new ValidateHelper<string?>();

            ResponseGeneralModel<string?> nameUser = validaH.ValidResp(model.userName, "userName", ListRegExp: new List<string>() { VarHelper.regExParamString });
            if (nameUser.code != 200) return nameUser;

            return new ResponseGeneralModel<string?>(200, "");
        }
    }
}