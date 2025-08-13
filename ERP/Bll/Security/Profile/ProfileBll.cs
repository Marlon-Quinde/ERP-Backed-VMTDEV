using Azure.Core;
using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Helper;
using ERP.Helper.Models;
using ERP.Models.Security.Profile;
using Newtonsoft.Json;

namespace ERP.Bll.Security.Profile
{
    public class ProfileBll : IProfileBll
    {
        BaseErpContext _context;
        IHttpContextAccessor _httpContext;

        SessionModel sessMod;

        public ProfileBll(BaseErpContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;

            string token = _httpContext.HttpContext.Request.Headers["token"];
            if (token == "") return;
            MethodsHelper<ProfileResponseModel> metHel = new MethodsHelper<ProfileResponseModel>();
            ResponseGeneralModel<ProfileResponseModel> decToken = metHel.DecodeJwtSession(token);


            if (decToken != null)
            {
                if (decToken.code == 200)
                {
                    sessMod = JsonConvert.DeserializeObject<SessionModel>(decToken.message);
                }
            }
        }

        public ResponseGeneralModel<ProfileResponseModel> Me()
        {

            var dataUs = (from us in _context.Usuarios
                          join comp in _context.Empresas on us.EmpresaId equals comp.EmpresaId
                          select new
                          {
                              us,
                              comp
                          }).FirstOrDefault((item) => item.us.UsuId == sessMod.id);

            ProfileResponseModel profResp = new ProfileResponseModel()
            {
                name = dataUs.us.UsuNombre,
                email = dataUs.us.Email,
                companyName = dataUs.comp.EmpresaNombre,
            };

            return new ResponseGeneralModel<ProfileResponseModel>(200, profResp, "");
        }

        public ResponseGeneralModel<string?> ChangePasswordUser(ChangePasswordRequestModel requestModel)
        {
            ResponseGeneralModel<string> oldPassword = MethodsHelper<string>.EncryptPassUser(requestModel.oldPassword);
            ResponseGeneralModel<string> newPassword = MethodsHelper<string>.EncryptPassUser(requestModel.newPassword);
            if (newPassword.code != 200) return newPassword;

            Usuario? user = _context.Usuarios.FirstOrDefault((item) => item.UsuId == sessMod.id && item.Clave == oldPassword.message);

            if(user == null)
            {
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.profileChangePasswordErrorNotEqualsOldPassword, MessageHelper.profileChangePasswordErrorNotEqualsOldPassword);
            }

            user.Clave = newPassword.message;

            _context.SaveChanges();

            return new ResponseGeneralModel<string?>(200, MessageHelper.profileChangePasswordSuccess);
        }

        public ResponseGeneralModel<string?> ChangeNameUser(ChangeNameUserRequestModel requestModel)
        {
            Usuario user = _context.Usuarios.First((item) => item.UsuId == sessMod.id);

            user.UsuNombre = requestModel.userName;

            _context.SaveChanges();

            return new ResponseGeneralModel<string?>(200, MessageHelper.profileChangeNameUserSuccess);
        }
    }
}
