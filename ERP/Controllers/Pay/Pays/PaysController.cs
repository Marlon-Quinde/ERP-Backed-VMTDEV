using ERP.Bll.Pay;
using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Models;
using ERP.Models.Pay.CreditCard;
using ERP.Models.Pay.FormPay;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP.Controllers.Pay.Pays
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaysController : ControllerBase
    {
        private readonly IFormPayBll formPayBll;

        public PaysController(IFormPayBll bll)
        {
            formPayBll = bll;
        }

        [HttpGet("Listar-FormaPago")]
        public ResponseGeneralModel<object?> GetFormPay()
        {
            try
            {
                return new ResponseGeneralModel<object?>(200, formPayBll.GetFormPago(), MessageHelper.FormPayCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<object?>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }

        [HttpPost("Crear-FormaPago")]
        public ResponseGeneralModel<string?> CreateFormPay([FromBody] FormPayRequestModel request)
        {
            return formPayBll.CreateFormPago(request);
        }

        [HttpPut("Actualizar-FormaPago/{id}")]  
        public ResponseGeneralModel<bool?> PutFormPago(int id, [FromBody] FormPayRequestModel requestModel)
        {
            try
            {
                return formPayBll.EditFormPago(id, requestModel);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }

        //movementdetProducto

        [HttpGet("listar-TarjetaCredito")]
        public ResponseGeneralModel<object?> GetCreditCard()
        {
            try
            {
                return new ResponseGeneralModel<object?>(200, formPayBll.GetCreditCard(), MessageHelper.CreditCardCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<object?>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }


        [HttpPost("Crear-TarjetaCredito")]
        public ResponseGeneralModel<string?> CreateCreditCard([FromBody] CreditCardRequestModel request)
        {
            return formPayBll.CreateCreditCard(request);
        }

        [HttpPut("Actualizar-TarjetaCredito/{id}")]
        public ResponseGeneralModel<bool?> PutCreditCard(int id, [FromBody] CreditCardRequestModel requestModel)
        {
            try
            {
                return formPayBll.EditCreditCard(id, requestModel);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }

    }
}
