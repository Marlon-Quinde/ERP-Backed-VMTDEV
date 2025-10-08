using ERP.Bll.Products.Product;
using ERP.Bll.Products.Stock;
using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Models;
using ERP.Models.Products.Product;
using ERP.Models.Products.Stock;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP.Controllers.Products.Stock
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockBll stockBll;

        public StockController(IStockBll bll)
        {
            stockBll = bll;
        }

        [HttpGet("Listar-Stock")]
        public ResponseGeneralModel<object?> GetStock()
        {
            try
            {
                return new ResponseGeneralModel<object?>(
                    200,
                    stockBll.GetAllStock(),
                    MessageHelper.StockCorrect
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

        [HttpPost("Crear-Stock")]
        public ResponseGeneralModel<string?> CreateStock([FromBody] StockRequestModel request)
        {
            return stockBll.CreateStock(request);
        }

        [HttpPut("Actualizar-Stock/{id}")]
        public ResponseGeneralModel<bool?> EditStock(int id, [FromBody] StockRequestModel requestModel)
        {
            try
            {
                return stockBll.EditStock(id, requestModel);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(
                    500,
                    null,
                    MessageHelper.errorGeneral,
                    e.ToString()
                );
            }
        }
    }
}
