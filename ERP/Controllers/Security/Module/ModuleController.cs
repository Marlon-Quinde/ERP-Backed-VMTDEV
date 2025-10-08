using ERP.Bll.Security.Module;
using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Models;
using ERP.Models.Security.Module;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP.Controllers.Security.Module
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly IModuleBll moduleBll;

        public ModuleController(IModuleBll bll)
        {
            moduleBll = bll;
        }

        [HttpGet("Listar-Módulo")]
        public ResponseGeneralModel<List<Modulo>?> GetModule()
        {
            try
            {
                return new ResponseGeneralModel<List<Modulo>?>(200, moduleBll.GetModule(), MessageHelper.ModuleCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<List<Modulo>?>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }

        [HttpPost("Crear-Módulo")]
        public ResponseGeneralModel<string?> CreateModule([FromBody] ModuleRequestModel request)
        {
            return moduleBll.CreateModule(request);
        }

        [HttpPut("Actualizar-Módulo")]
        public ResponseGeneralModel<bool?> Put(int id, [FromBody] ModuleRequestModel requestModel)
        {
            try
            {
                return moduleBll.EditModule(id, requestModel);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }
    }
}
