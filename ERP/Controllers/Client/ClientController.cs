using ERP.Bll.Empresa;
using ERP.Bll.Security.Authentication;
using ERP.Bll.User;
using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Models;
using ERP.Models.Security.Authentication;
using ERP.Models.test;
using ERP.Validate.Security;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP.Controllers.Empresa
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        IClientBll clientBll;
        public ClientController(IClientBll bll)
        {
            this.clientBll = bll;
        }

       
        [HttpGet("listar")]
        public ResponseGeneralModel<List<Cliente>?> TestDb()
        {
            try
            {
                return new ResponseGeneralModel<List<Cliente>?>(200, clientBll.GetCliente(), MessageHelper.clientListCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<List<Cliente>?>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }
        [HttpPost("crear")]
        public ResponseGeneralModel<string?> Post([FromBody] ClientRequestModel requestModel)
        {
            try
            {
                ResponseGeneralModel<string?> isOk = clientBll.CreateClient(requestModel);
                return isOk;
            }
            catch
            {
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.errorGeneral);
            }
        }
    }
}
