using ERP.Bll.Company.Warehouse;
using ERP.Bll.Invoice.MovementInvoice;
using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Models;
using ERP.Models.Company.Warehouse;
using ERP.Models.Inventory.Warehouse;
using ERP.Models.Invoice.MovemetCab;
using ERP.Models.Invoice.MovemetDetPay;
using ERP.Models.Invoice.MovemetDetProduct;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP.Controllers.Invoice.Movements
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovementsController : ControllerBase
    {
        private readonly IMovementInvoiceBll movementInvoiceBll;

        public MovementsController(IMovementInvoiceBll bll)
        {
            movementInvoiceBll = bll;
        }

        [HttpGet("Listar-MovimientoCab")]
        public ResponseGeneralModel<object?> GetMovementCab()
        {
            try
            {
                return new ResponseGeneralModel<object?>(200, movementInvoiceBll.GetMovementCab(), MessageHelper.MovementCabCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<object?>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }

        [HttpPost("Crear-MovimientoCab")]
        public ResponseGeneralModel<string?> CreateMovementCab([FromBody] MovemetCabRequestModel request)
        {
            return movementInvoiceBll.CreateMovemetCab(request);
        }

        [HttpPut("Actualizar-MovimientoCab/{movimientoCabId}")] 
        public ResponseGeneralModel<bool?> PutMovimientoCab(int movimientoCabId, [FromBody] MovemetCabRequestModel requestModel)
        {
            try
            {
                return movementInvoiceBll.EditMovementCab(movimientoCabId, requestModel);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }

        //movementdetProducto

        [HttpGet("listar-MovementProducto")]
        public ResponseGeneralModel<object> GetMovementProducto()
        {
            try
            {
                var data = movementInvoiceBll.GetMovementDetProduct();
                return new ResponseGeneralModel<object>(200, data, MessageHelper.MovementDetProductCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<object>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }


        [HttpPost("Crear-MovimientoDetProduct")]
        public ResponseGeneralModel<string?> CreateMovementProduct([FromBody] MovemetDetProductRequestModel request)
        {
            return movementInvoiceBll.CreateMovemetDetProduct(request);
        }

        [HttpPut("Actualizar-MovimientoDetProduct/{movimientoDetProductId}")]  
        public ResponseGeneralModel<bool?> PutMovimientoDetProduct(int movimientoDetProductId, [FromBody] MovemetDetProductRequestModel requestModel)
        {
            try
            {
                return movementInvoiceBll.EditMovementDetProduct(movimientoDetProductId, requestModel);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }


        [HttpGet("listar-MovimientoDetPay")]
        public ResponseGeneralModel<object?> GetMovementDetPay()
        {
            try
            {
                return new ResponseGeneralModel<object?>(200, movementInvoiceBll.GetMovementDetPay(), MessageHelper.MovementPayCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<object?>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }

        [HttpPost("Crear-MovimientoDetPay")]
        public ResponseGeneralModel<string?> CreateMovementPay([FromBody] MovemetDetPayRequestModel request)
        {
            return movementInvoiceBll.CreateMovementDetPay(request);
        }

        [HttpPut("Actualizar-MovimientoDetPay/{movimientoDetPayId}")] 
        public ResponseGeneralModel<bool?> PutMovimientoDetPay(int movimientoDetPayId, [FromBody] MovemetDetPayRequestModel requestModel)
        {
            try
            {
                return movementInvoiceBll.EditMovementDetPay(movimientoDetPayId, requestModel);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }
    }
}
