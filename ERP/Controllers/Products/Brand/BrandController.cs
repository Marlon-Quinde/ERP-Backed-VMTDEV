using ERP.Bll.Commercial.Customer;
using ERP.Bll.Pay;
using ERP.Bll.Products.Brand;
using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Models;
using ERP.Models.Products.Brand;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP.Controllers.Products.Brand
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandBll brandBll;

        public BrandController(IBrandBll bll)
        {
            brandBll = bll;
        }

        [HttpGet("Listar-Marca")]
        public ResponseGeneralModel<object?> GetBrand()
        {
            try
            {

                return new ResponseGeneralModel<object?>(200, brandBll.GetBrand(), MessageHelper.BrandCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<object?>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }

        [HttpPost("Crear-Marca")]
        public ResponseGeneralModel<string?> CreateBrand([FromBody] BrandRequestModel request)
        {
            return brandBll.CreateBrand(request);
        }

        [HttpPut("Actualizar-Marca")]
        public ResponseGeneralModel<bool?> Put(int id, [FromBody] BrandRequestModel requestModel)
        {
            try
            {
                return brandBll.EditBrand(id, requestModel);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }
        [HttpDelete("{id}")]
        public ResponseGeneralModel<bool?> DeleteBrand(int id)
        {
            try
            {
                return brandBll.DeleteBrand(id);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }


    }
}
