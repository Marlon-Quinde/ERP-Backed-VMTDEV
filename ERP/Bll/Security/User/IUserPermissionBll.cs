using ERP.CoreDB;
using ERP.Helper.Models;
using ERP.Models.Security.User;

namespace ERP.Bll.Security.User
{
    public interface IUserPermissionBll
    {
        ResponseGeneralModel<string?> CreateUserPermission(UserPermissionRequestModel request);
        ResponseGeneralModel<bool?> EditUserPermission(int id, UserPermissionRequestModel request);
        ResponseGeneralModel<bool?> DeleteUserPermission(int id);
        public List<UsuarioPermiso> GetUserPermissions();

    }
}
