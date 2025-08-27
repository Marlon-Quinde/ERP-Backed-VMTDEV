using ERP.Bll.Empresa;
using ERP.Bll.Role;
using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Models;
using ERP.Models.Role;
using ERP.Models.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP.Controllers.Role
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        IRoleBll roleBll;
        public RoleController(IRoleBll bll)
        {
            this.roleBll = bll;
        }
        

        [HttpGet("listar")]
        public ResponseGeneralModel<List<Rol>?> TestDb()
        {
            try
            {
                return new ResponseGeneralModel<List<Rol>?>(200, roleBll.GetRol(), MessageHelper.roleListCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<List<Rol>?>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }

        // POST api/<RoleController>
        [HttpPost("crear")]
        public ResponseGeneralModel<string?> Post([FromBody] RoleRequestmodel requestModel)
        {
            try
            {
                ResponseGeneralModel<string?> isOk = roleBll.CreateRole(requestModel);
                return isOk;
            }
            catch
            {
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.errorGeneral);
            }
        }

        // PUT api/<RoleController>/5
        [HttpPut("{id}")]
        public ResponseGeneralModel<bool?> Put(int id,[FromBody] EditRoleRequestModel requestModel)
        {
            try
            {
                return roleBll.EditRole(id, requestModel);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }

        // DELETE api/<RoleController>/5
        [HttpDelete("{id}")]
        public ResponseGeneralModel<bool?> Delete(int id)
        {
            try
            {
                return roleBll.DeleteRole(id);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }
    }
}
