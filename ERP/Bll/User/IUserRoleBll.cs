using ERP.CoreDB;
using ERP.Helper.Models;
using ERP.Models.Security.User;

namespace ERP.Bll.UserRole
{
    public interface IUserRoleBll
    {
        ResponseGeneralModel<bool> AssignRole(UserRoleRequestModel request);
        ResponseGeneralModel<bool> RemoveRole(UserRoleRequestModel request);
        List<UsuarioRol> GetUserRol();
    }
};
