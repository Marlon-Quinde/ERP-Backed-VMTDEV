using ERP.Bll.Products.Industry;
using ERP.Bll.Products.Product;
using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Models;
using ERP.Models.Products.Industry;
using ERP.Models.Products.Product;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP.Controllers.Products.Industry
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndustryController : ControllerBase
    {
        private readonly IIndustryBll industryBll;

        public IndustryController(IIndustryBll bll)
        {
            industryBll = bll;
        }

        [HttpGet("Listar-Industria")]
        public ResponseGeneralModel<object?> GetIndustry()
        {
            try
            {
                return new ResponseGeneralModel<object?>(200, industryBll.GetIndustry(), MessageHelper.IndustryCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<object?>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }

        [HttpPost("Crear-Industria")]
        public ResponseGeneralModel<string?> CreateIndustry([FromBody] IndustryRequestModel request)
        {
            return industryBll.CreateIndustry(request);
        }

        [HttpPut("Actualizar-Industria")]
        public ResponseGeneralModel<bool?> Put(int id, [FromBody] IndustryRequestModel requestModel)
        {
            try
            {
                return industryBll.EditIndustry(id, requestModel);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }
    }
}
