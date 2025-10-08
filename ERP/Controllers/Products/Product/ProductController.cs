using ERP.Bll.Products.Category;
using ERP.Bll.Products.Product;
using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Models;
using ERP.Models.Products.Category;
using ERP.Models.Products.Product;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP.Controllers.Products.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductBll productBll;

        public ProductController(IProductBll bll)
        {
            productBll = bll;
        }

        [HttpGet("Listar-Productos")]
        public ResponseGeneralModel<object?> GetProduct()
        {
            try
            {
                return new ResponseGeneralModel<object?>(
                    200,
                    productBll.GetProduct(), 
                    MessageHelper.ProductCorrect
                );
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<object?>(
                    500,
                    null,
                    MessageHelper.errorGeneral,
                    ex.ToString()
                );
            }
        }
        [HttpPost("Crear-Productos")]
        public ResponseGeneralModel<string?> CreateProduct([FromBody] ProductRequestModel request)
        {
            return productBll.CreateProduct(request);
        }

        [HttpPut("Actualizar-Productos")]
        public ResponseGeneralModel<bool?> Put(int id, [FromBody] ProductRequestModel requestModel)
        {
            try
            {
                return productBll.EditProduct(id, requestModel);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }

        [HttpDelete("{id}")]
        public ResponseGeneralModel<bool?> DeleteProduct(int id)
        {
            try
            {
                return productBll.DeleteProduct(id);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }
    }
}
