using ERP.Bll.Role;
using ERP.Bll.UserRole;
using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Models;
using ERP.Models.Security.User;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleBll _userRoleBll;

        public UserRoleController(IUserRoleBll userRoleBll)
        {
            _userRoleBll = userRoleBll;
        }

        [HttpGet("UserRol")]
        public ResponseGeneralModel<List<UsuarioRol>?> TestDb()
        {
            try
            {
                return new ResponseGeneralModel<List<UsuarioRol>?>(200, _userRoleBll.GetUserRol(), "");
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<List<UsuarioRol>?>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }
        [HttpPost("Asignar Rol")]
        public ResponseGeneralModel<bool> AssignRole([FromBody] UserRoleRequestModel requestModel)
        {
            try
            {
                return _userRoleBll.AssignRole(requestModel);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<bool>(500, false, MessageHelper.errorGeneral, ex.ToString());
            }
        }

        [HttpPost("Eliminar Rol")]
        public ResponseGeneralModel<bool> RemoveRole([FromBody] UserRoleRequestModel requestModel)
        {
            try
            {
                return _userRoleBll.RemoveRole(requestModel);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<bool>(500, false, MessageHelper.errorGeneral, ex.ToString());
            }
        }
    }
}
