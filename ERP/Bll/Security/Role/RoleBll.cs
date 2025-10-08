using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Helper;
using ERP.Helper.Models;
using ERP.Models.Security.Authentication;
using ERP.Models.Security.Profile;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using ERP.Models.Security.Role;

namespace ERP.Bll.Security.Role
{
    public class RoleBll : IRoleBll
    {
        private readonly BaseErpContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private SessionModel sessMod;

        public RoleBll(BaseErpContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;

            // 🔒 Verificación del token
            string token = _httpContext.HttpContext.Request.Headers["token"];
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token requerido");
            }

            MethodsHelper<RoleRequestmodel> metHel = new MethodsHelper<RoleRequestmodel>();
            ResponseGeneralModel<RoleRequestmodel> decToken = metHel.DecodeJwtSession(token);

            if (decToken != null && decToken.code == 200)
            {
                sessMod = JsonConvert.DeserializeObject<SessionModel>(decToken.message);
            }
            else
            {
                throw new UnauthorizedAccessException("Token inválido o expirado");
            }
        }

        public ResponseGeneralModel<string?> CreateRole(RoleRequestmodel request)
        {
            try
            {
                _context.Database.BeginTransaction();
                var roleExist = _context.Rols
                    .FirstOrDefault(p => p.RolDescripcion.ToUpper() == request.RoleDescrip.ToUpper());

                if (roleExist == null)
                {
                    var ultimRole = _context.Rols.OrderByDescending(p => p.RolId).FirstOrDefault();
                    int nuevoRoleId = ultimRole == null ? 1 : ultimRole.RolId + 1;

                    roleExist = new Rol
                    {
                        RolId = nuevoRoleId,
                        RolDescripcion = request.RoleDescrip,
                        Estado = 1
                    };
                    _context.Rols.Add(roleExist);
                    _context.SaveChanges();
                }

                _context.Database.CommitTransaction();

                return new ResponseGeneralModel<string?>(200, MessageHelper.roleCorrect);
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.roleIncorrect, ex.ToString());
            }
        }

        public List<Rol> GetRol()
        {
            return _context.Rols.ToList();
        }

        public ResponseGeneralModel<bool?> EditRole(int id, EditRoleRequestModel requestModel)
        {
            Rol roles = _context.Rols.First((item) => item.RolId == id);

            roles.RolDescripcion = requestModel.RolDescrip;
            _context.SaveChanges();

            return new ResponseGeneralModel<bool?>(200, MessageHelper.profileChangeRolUserSuccess);
        }

        public ResponseGeneralModel<bool?> DeleteRole(int id)
        {
            try
            {
                Rol? role = _context.Rols.FirstOrDefault(r => r.RolId == id);
                if (role == null)
                {
                    return new ResponseGeneralModel<bool?>(404, null, MessageHelper.roleNotFound);
                }

                // Eliminado lógico
                role.Estado = 0;
                _context.SaveChanges();

                return new ResponseGeneralModel<bool?>(200, true, MessageHelper.roleDelete);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.roleErrorDelete, ex.ToString());
            }
        }
    }
}
