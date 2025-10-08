using ERP.Bll.Products.Product;
using ERP.Bll.Products.Tax;
using ERP.Helper.Data;
using ERP.Helper.Models;
using ERP.Models.Products.Product;
using ERP.Models.Products.Tax;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP.Controllers.Products.Tax
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private readonly ITaxBll taxBll;

        public TaxController(ITaxBll bll)
        {
            taxBll = bll;
        }

        [HttpGet("Listar-Impuestos")]
        public ResponseGeneralModel<object?> GetImpuesto()
        {
            try
            {
                return new ResponseGeneralModel<object?>(200, taxBll.GetTax(), MessageHelper.TaxCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<object?>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }

        [HttpPost("Crear-Impuestos")]
        public ResponseGeneralModel<string?> CreateTax([FromBody] TaxRequestModel request)
        {
            return taxBll.CreateTax(request);
        }

        [HttpPut("Actualizar-Impuestos")]
        public ResponseGeneralModel<bool?> Put(int id, [FromBody] TaxRequestModel requestModel)
        {
            try
            {
                return taxBll.EditTax(id, requestModel);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }
    }
}
