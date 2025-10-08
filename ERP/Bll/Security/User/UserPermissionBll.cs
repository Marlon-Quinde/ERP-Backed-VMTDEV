using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Helper;
using ERP.Helper.Models;
using ERP.Models.Security.User;
using Newtonsoft.Json;

namespace ERP.Bll.Security.User
{
    public class UserPermissionBll : IUserPermissionBll
    {
        private readonly BaseErpContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private SessionModel sessMod;

        public UserPermissionBll(BaseErpContext context, IHttpContextAccessor httpContext)
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

        public ResponseGeneralModel<string?> CreateUserPermission(UserPermissionRequestModel request)
        {
            try
            {
                _context.Database.BeginTransaction();

                var permExist = _context.UsuarioPermisos
                    .FirstOrDefault(p => p.UsuId == request.UsuId &&
                                         p.ModuloId == request.ModulId &&
                                         p.OpcionId == request.OptionId &&
                                         p.EstadoPermiso == 1);

                if (permExist != null)
                    return new ResponseGeneralModel<string?>(400, null, MessageHelper.PermissionCorrect);

                var lastPerm = _context.UsuarioPermisos
                    .OrderByDescending(p => p.PermisoId)
                    .FirstOrDefault();

                int newPermId = (lastPerm == null) ? 1 : lastPerm.PermisoId + 1;

                var permiso = new UsuarioPermiso
                {
                    PermisoId = newPermId,
                    ModuloId = request.ModulId,
                    OpcionId = request.OptionId,
                    UsuId = request.UsuId ?? 0,
                    EstadoPermiso = 1,
                    UsuIdReg = sessMod.id,
                    FechaHoraReg = DateTime.Now
                };

                _context.UsuarioPermisos.Add(permiso);
                _context.SaveChanges();

                _context.Database.CommitTransaction();
                return new ResponseGeneralModel<string?>(200, MessageHelper.PermissionCorrect);
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.PermissionIncorrect, ex.ToString());
            }
        }

        public ResponseGeneralModel<bool?> EditUserPermission(int id, UserPermissionRequestModel request)
        {
            var permiso = _context.UsuarioPermisos.FirstOrDefault(p => p.PermisoId == id && p.EstadoPermiso == 1);
            if (permiso == null)
                return new ResponseGeneralModel<bool?>(404, null, MessageHelper.PermissionNotFound);

            permiso.ModuloId = request.ModulId ?? permiso.ModuloId;
            permiso.OpcionId = request.OptionId ?? permiso.OpcionId;
            permiso.UsuId = request.UsuId ?? permiso.UsuId;
            permiso.UsuIdAct = sessMod.id;
            permiso.FechaHoraAct = DateTime.Now;

            _context.SaveChanges();

            return new ResponseGeneralModel<bool?>(200, MessageHelper.PermissionEdit);
        }

        public ResponseGeneralModel<bool?> DeleteUserPermission(int id)
        {
            var permiso = _context.UsuarioPermisos.FirstOrDefault(p => p.PermisoId == id && p.EstadoPermiso == 1);
            if (permiso == null)
                return new ResponseGeneralModel<bool?>(404, null, MessageHelper.PermissionNotFound);

            permiso.EstadoPermiso = 0;
            permiso.UsuIdAct = sessMod.id;
            permiso.FechaHoraAct = DateTime.Now;

            _context.SaveChanges();

            return new ResponseGeneralModel<bool?>(200, MessageHelper.PermissionDelete);
        }
        public List<UsuarioPermiso> GetUserPermissions()
        {
            return _context.UsuarioPermisos
                .Where(p => p.EstadoPermiso == 1)
                .ToList();
        }
    }
}
