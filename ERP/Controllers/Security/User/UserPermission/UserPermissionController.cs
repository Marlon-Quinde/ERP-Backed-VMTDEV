using ERP.Bll.Security.Role;
using ERP.Bll.Security.User;
using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Models;
using ERP.Models.Security.Role;
using ERP.Models.Security.User;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP.Controllers.Security.User.UserPermission
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPermissionController : ControllerBase
    {
        private readonly IUserPermissionBll userPermissionBll;

        public UserPermissionController(IUserPermissionBll userPermissionBll)
        {
            userPermissionBll = userPermissionBll;
        }

        [HttpGet("Listar-Permiso")]
        public ResponseGeneralModel<List<UsuarioPermiso>?> testdb()
        {
            try
            {
                return new ResponseGeneralModel<List<UsuarioPermiso>?>(200, userPermissionBll.GetUserPermissions(), MessageHelper.PermissionCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<List<UsuarioPermiso>?>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }
        [HttpPost("Crear-Permiso")]
        public ResponseGeneralModel<string?> Post([FromBody] UserPermissionRequestModel requestModel)
        {
            try
            {
                ResponseGeneralModel<string?> isOk = userPermissionBll.CreateUserPermission(requestModel);
                return isOk;
            }
            catch
            {
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.errorGeneral);
            }
        }
        [HttpPut("Actualizar-Permiso")]

        public ResponseGeneralModel<bool?> Put(int id, [FromBody] UserPermissionRequestModel requestModel)
        {
            try
            {
                return userPermissionBll.EditUserPermission(id, requestModel);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }

        [HttpDelete("{id}")]
        public ResponseGeneralModel<bool?> DeletePermission(int id)
        {
            try
            {
                return userPermissionBll.DeleteUserPermission(id);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }
    }
}
