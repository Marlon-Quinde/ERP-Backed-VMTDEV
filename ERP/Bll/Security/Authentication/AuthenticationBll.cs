using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Helper;
using ERP.Helper.Models;
using ERP.Helper.Models.WorkerProcess;
using ERP.Models.Security.Authentication;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace ERP.Bll.Security.Authentication
{
    public class AuthenticationBll : IAuthenticationBll
    {
        BaseErpContext _context;
        IConfiguration _configuration;
        public AuthenticationBll(BaseErpContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public ResponseGeneralModel<LoginResponseModel?> Login(LoginRequestModel requestModel)
        {
            //Usuario? userFind = _context.Usuarios.FirstOrDefault((user) => user.Email == requestModel.email && user.Clave == requestModel.password);
            //if (userFind == null) {
            //    return new ResponseGeneralModel<LoginResponseModel?>(404, null, MessageHelper.loginIncorrect);
            //}

            //Empresa companyFind = _context.Empresas.First((item) => item.EmpresaId == userFind.EmpresaId);

            //LoginResponseModel loginModel = new LoginResponseModel();
            //loginModel.id = userFind.UsuId;
            //loginModel.name = userFind.UsuNombre;
            //loginModel.companyName = companyFind.EmpresaNombre;

            //return new ResponseGeneralModel<LoginResponseModel?>(200, loginModel, "");

            ResponseGeneralModel<LoginResponseModel?> keyPassword = MethodsHelper<LoginResponseModel?>.EncryptPassUser(requestModel.password);
            if (keyPassword.code != 200) return keyPassword;


            var dataUs = (from us in _context.Usuarios
                          join comp in _context.Empresas on us.EmpresaId equals comp.EmpresaId
                          select new
                          {
                              us,
                              comp
                          }).FirstOrDefault((item) => item.us.Email == requestModel.email && item.us.Clave == keyPassword.message);

            if (dataUs == null)
            {
                return new ResponseGeneralModel<LoginResponseModel?>(404, null, MessageHelper.loginIncorrect);
            }
            var roles = (from ur in _context.UsuarioRols
                         join r in _context.Rols on ur.RolId equals r.RolId
                         where ur.UsuId == dataUs.us.UsuId
                         select r.RolDescripcion).ToList();
            SessionModel sessionModel = new SessionModel(dataUs.us.UsuId, dataUs.us.UsuNombre);

            ResponseGeneralModel<LoginResponseModel?> jwt = (new MethodsHelper<LoginResponseModel?>()).GenerateJwtSession(sessionModel, roles);
            if (jwt.code != 200) return jwt;

            LoginResponseModel loginModel = new LoginResponseModel();
            loginModel.id = dataUs.us.UsuId;
            loginModel.name = dataUs.us.UsuNombre;
            loginModel.companyName = dataUs.comp.EmpresaNombre;
            loginModel.jwt = jwt.message;
            loginModel.roles = roles;


            return new ResponseGeneralModel<LoginResponseModel?>(200, loginModel, "");
        }

        public ResponseGeneralModel<bool> Register(RegisterRequestModel requestModel)
        {
            Usuario? usuarioExist = _context.Usuarios.FirstOrDefault((item) => item.Email == requestModel.email);
            if (usuarioExist != null)
            {
                return new ResponseGeneralModel<bool>(400, false, MessageHelper.registerErrorExist);
            }

            Usuario? lastUser = _context.Usuarios.OrderByDescending((item) => item.UsuId).FirstOrDefault();
            ResponseGeneralModel<bool> keyPassword = MethodsHelper<bool>.EncryptPassUser(requestModel.password);
            if (keyPassword.code != 200) return keyPassword;
            Usuario userModel = new Usuario()
            {
                UsuId = lastUser != null ? (lastUser.UsuId + 1) : 1,
                UsuNombre = requestModel.name,
                Email = requestModel.email,
                Clave = keyPassword.message,
                EmpresaId = requestModel.companyId,
                Estado = 1,
            };

            _context.Usuarios.Add(userModel);
            _context.SaveChanges();


            string bodyMail = (new TemplateHtmlHelper()).EmailCreateUser(requestModel.name, requestModel.email);

            (new MongoMethods<WorkerProcessMailModel>()).SaveProcessMail(new SmtpSendRequestModel()
            {
                To = "jmoran@viamatica.com",
                Subject = "Creacion de usuario",
                Body = bodyMail,
            });

            //ExternalServiceHelper extSerH = new ExternalServiceHelper();
            //SmtpSendRequestModel smtpObjEmail = new SmtpSendRequestModel()
            //{
            //    To = "jmoran@viamatica.com",
            //    Subject = "Creacion de usuario",
            //    Body = bodyMail
            //};
            //extSerH.PostServiceExternal(_configuration.GetSection("services").GetValue<string>("smtp"), jsonData: smtpObjEmail.ToJson());

            return new ResponseGeneralModel<bool>(200, true, "");
        }
    }
}