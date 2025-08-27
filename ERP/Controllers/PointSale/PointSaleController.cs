using ERP.Bll.Empresa;
using ERP.Bll.PointOfIssueBll;
using ERP.Bll.PointSaleBll;
using ERP.Bll.PuntoVenta;
using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Models;
using ERP.Models.PointOfIssue;
using ERP.Models.PointOfSale;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP.Controllers.PointSale
{
    [ApiController]
    [Route("api/[controller]")]
    public class PointSaleController : ControllerBase
    {
        private readonly IPointSaleBll _pointSaleBll;
        public PointSaleController(IPointSaleBll pointSaleBll)
        {
            _pointSaleBll = pointSaleBll;
        }

        // GET api/PointSale/List
        [HttpGet("List")]
        public ResponseGeneralModel<List<PointSaleResponseModel>> GetPointSales()
        {
            try
            {
                return _pointSaleBll.GetPointSales();
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<List<PointSaleResponseModel>>(
                    500,
                    null,
                   MessageHelper.errorGeneral,
                    ex.ToString()
                );
            }
        }

        // POST api/PointSale/Create
        [HttpPost("Create")]
        public ResponseGeneralModel<string?> CreatePointSale([FromBody] PointSaleRequestModel request)
        {
            return _pointSaleBll.CreatePointSale(request);
        }
    }
} 