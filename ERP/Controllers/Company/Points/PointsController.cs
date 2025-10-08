using ERP.Bll.Company.Company;
using ERP.Bll.Company.Point;
using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Models;
using ERP.Models.Company.Company;
using ERP.Models.Company.Point.PointEmissionSri;
using ERP.Models.Company.Point.PointSale;
using ERP.Models.Inventory.Company;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP.Controllers.Commercial.Points
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointsController : ControllerBase
    {
        private readonly IPointBll pointBll;

        public PointsController(IPointBll bll)
        {
            pointBll = bll;
        }

        [HttpGet("listar-punto-emission-sri")]
        public ResponseGeneralModel<object?> GetPointEmission()
        {
            try
            {
                return new ResponseGeneralModel<object?>(200, pointBll.GetPuntoEmisionSri(), MessageHelper.pointEmissionCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<object?>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }

        [HttpPost("crear-punto-emission")]
        public ResponseGeneralModel<string?> CreatePointEmission([FromBody] PointEmissionRequestModel request)
        {
            return pointBll.CreatePointEmission(request);
        }

        [HttpPut("editar-punto-emission/{puntoEmissionId}")] 
        public ResponseGeneralModel<bool?> PutPointEmission(int puntoEmissionId, [FromBody] EditPointEmissionRequestModel requestModel)
        {
            try
            {
                var result = pointBll.EditPointEmission(puntoEmissionId, requestModel);
                if (result.code == 400 || result.code == 404)
                {
                    return result;
                }
                return result;
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }
            // Puntos de Venta
        [HttpGet("listar-punto-venta")]
        public ResponseGeneralModel<object?> GetPointSale()
        {
            try
            {
                return new ResponseGeneralModel<object?>(200, pointBll.GetPuntoVenta(), MessageHelper.pointSaleCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<object?>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }

        [HttpPost("crear-punto-venta")]
        public ResponseGeneralModel<string?> CreatePointSale([FromBody] PointSaleRequestModel request)
        {
            return pointBll.CreatePointSale(request);
        }

        [HttpPut("editar-punto-venta/{puntoVentaId}")]
        public ResponseGeneralModel<bool?> PutPointSale(int puntoVentaId, [FromBody] EditPointSaleRequestModel requestModel)
        {
            try
            {
                var result = pointBll.EditPointSale(puntoVentaId, requestModel);
                if (result.code == 400 || result.code == 404)
                {
                    return result;
                }
                return result;
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }
        }
}
