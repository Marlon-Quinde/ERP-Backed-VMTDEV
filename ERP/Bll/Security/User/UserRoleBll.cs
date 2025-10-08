using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Helper;
using ERP.Helper.Models;
using ERP.Models.Security.User;
using Newtonsoft.Json;

namespace ERP.Bll.Security.User
{
    public class UserRoleBll : IUserRoleBll
    {
        private readonly BaseErpContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private SessionModel sessMod;

        public UserRoleBll(BaseErpContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;

            string token = _httpContext.HttpContext.Request.Headers["token"];
            if (string.IsNullOrEmpty(token))
                throw new UnauthorizedAccessException("Token requerido");

            MethodsHelper<object> metHel = new MethodsHelper<object>();
            var decToken = metHel.DecodeJwtSession(token);

            if (decToken != null && decToken.code == 200)
                sessMod = JsonConvert.DeserializeObject<SessionModel>(decToken.message);
            else
                throw new UnauthorizedAccessException("Token inválido o expirado");
        }
        public ResponseGeneralModel<bool> AssignRole(UserRoleRequestModel request)
        {
            try
            {
                var existing = _context.UsuarioRols
                    .FirstOrDefault(ur => ur.UsuId == request.UsuId && ur.RolId == request.RolId);

                if (existing != null)
                {
                    if (existing.Estado == 1)
                        return new ResponseGeneralModel<bool>(400, false, MessageHelper.roleIncorrect);
                    else
                    {
                        existing.Estado = 1;
                        existing.FechaHoraAct = DateTime.Now;
                    }
                }
                else
                {
                    var maxId = _context.UsuarioRols.Max(ur => (int?)ur.UsuRolId) ?? 0;

                    _context.UsuarioRols.Add(new UsuarioRol
                    {
                        UsuRolId = maxId + 1,
                        UsuId = request.UsuId,
                        RolId = request.RolId,
                        Estado = 1,
                        FechaHoraReg = DateTime.Now
                    });
                }

                _context.SaveChanges();

                return new ResponseGeneralModel<bool>(200, true, MessageHelper.roleCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<bool>(500, false, MessageHelper.errorGeneral, ex.ToString());
            }
        }


        public ResponseGeneralModel<bool> RemoveRole(UserRoleRequestModel request)
        {
            try
            {
                var existing = _context.UsuarioRols
                    .FirstOrDefault(ur => ur.UsuId == request.UsuId && ur.RolId == request.RolId);

                if (existing == null)
                {
                    return new ResponseGeneralModel<bool>(404, false, MessageHelper.roleErrorDelete);
                }

                existing.Estado = 0;
                existing.FechaHoraAct = DateTime.Now;

                _context.SaveChanges();

                return new ResponseGeneralModel<bool>(200, true,  MessageHelper.roleDelete);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<bool>(500, false, MessageHelper.errorGeneral, ex.ToString());
            }
        }

        public List<UsuarioRol> GetUserRol()
        {
            return _context.UsuarioRols.ToList();
        }
    }
}
