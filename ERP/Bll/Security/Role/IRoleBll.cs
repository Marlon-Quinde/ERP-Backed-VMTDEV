using ERP.CoreDB;
using ERP.Helper.Models;
using ERP.Models.Security.Role;

namespace ERP.Bll.Security.Role
{
    public interface IRoleBll
    {
        ResponseGeneralModel<string?> CreateRole(RoleRequestmodel request);
        public List<Rol> GetRol();
        ResponseGeneralModel<bool?> EditRole(int id, EditRoleRequestModel requestModel);
        ResponseGeneralModel<bool?> DeleteRole(int id);

    }
}
