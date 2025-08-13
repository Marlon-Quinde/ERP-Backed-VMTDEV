using ERP.Helper.Models;
using ERP.Models.Security.Profile;

namespace ERP.Bll.Security.Profile
{
    public interface IProfileBll
    {
        public ResponseGeneralModel<ProfileResponseModel> Me();
        public ResponseGeneralModel<string?> ChangePasswordUser(ChangePasswordRequestModel requestModel);
        public ResponseGeneralModel<string?> ChangeNameUser(ChangeNameUserRequestModel requestModel);
    }
}
