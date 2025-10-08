using ERP.Bll.Security.Option;
using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Models;
using ERP.Models.Security.Options;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP.Controllers.Security.Option
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : ControllerBase
    {
        private readonly IOptionBll optionBll;
        public OptionController(IOptionBll bll)
        {
            optionBll = bll;
        }

        [HttpGet("Listar-Opción")]
        public ResponseGeneralModel<List<Opcion>?> GetOption()
        {
            try
            {
                return new ResponseGeneralModel<List<Opcion>?>(200, optionBll.GetOption(), MessageHelper.OptionCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<List<Opcion>?>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }

        [HttpPost("Crear-Opción")]
        public ResponseGeneralModel<string?> CreateOption([FromBody] OptionRequestModel request)
        {
            return optionBll.CreateOption(request);
        }

        [HttpPut("Actualizar-Opción")]
        public ResponseGeneralModel<bool?> Put(int id, [FromBody] OptionRequestModel requestModel)
        {
            try
            {
                return optionBll.EditOption(id, requestModel);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }
    }
}
