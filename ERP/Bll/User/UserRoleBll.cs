using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Models;
using ERP.Models.Security.User;

namespace ERP.Bll.UserRole
{
    public class UserRoleBll : IUserRoleBll
    {
        private readonly BaseErpContext _context;

        public UserRoleBll(BaseErpContext context)
        {
            _context = context;
        }

        public ResponseGeneralModel<bool> AssignRole(UserRoleRequestModel request)
        {
            try
            {
                var existing = _context.UsuarioRols
                    .FirstOrDefault(ur => ur.UsuId == request.UsuarioId && ur.RolId == request.RolId);

                if (existing != null)
                {
                    if (existing.Estado == 1)
                        return new ResponseGeneralModel<bool>(400, false, "El usuario ya tiene este rol activo.");
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
                        UsuId = request.UsuarioId,
                        RolId = request.RolId,
                        Estado = 1,
                        FechaHoraReg = DateTime.Now
                    });
                }

                _context.SaveChanges();

                return new ResponseGeneralModel<bool>(200, true, "Rol asignado correctamente.");
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
                    .FirstOrDefault(ur => ur.UsuId == request.UsuarioId && ur.RolId == request.RolId);

                if (existing == null)
                {
                    return new ResponseGeneralModel<bool>(404, false, "El rol no está asignado a este usuario.");
                }

                existing.Estado = 0;
                existing.FechaHoraAct = DateTime.Now;

                _context.SaveChanges();

                return new ResponseGeneralModel<bool>(200, true, "Rol eliminado correctamente.");
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
