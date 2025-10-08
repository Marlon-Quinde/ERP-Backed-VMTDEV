using ERP.Bll.Commercial.Customer;
using ERP.Bll.Commercial.Supplier;
using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Models;
using ERP.Models.Commercial.Customer;
using ERP.Models.Commercial.Supplier;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP.Controllers.Commercial.Supplier
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierBll supplierBll;

        public SupplierController(ISupplierBll bll)
        {
            supplierBll = bll;
        }

        [HttpGet("listar-proveedor")]
        public ResponseGeneralModel<object?> TestDb()
        {
            try
            {
                return new ResponseGeneralModel<object?>(200, supplierBll.GetSupplier(), MessageHelper.SupplierCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<object?>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }
        [HttpPost("Crear-proveedor")]
        public ResponseGeneralModel<string?> Post([FromBody] SupplierRequestModel requestModel)
        {
            try
            {

                ResponseGeneralModel<string?> isOk = supplierBll.CreateSupplier(requestModel);
                return isOk;
            }
            catch
            {
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.errorGeneral);
            }
        }

        [HttpPut("Actualizar-Proveedor")]
        public ResponseGeneralModel<bool?> Put(int id, [FromBody] EditSupplierRequestModel requestModel)
        {
            try
            {
                return supplierBll.EditSupplier(id, requestModel);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }
        [HttpDelete("{id}")]
        public ResponseGeneralModel<bool?> DeleteSupplier(int id)
        {
            try
            {
                return supplierBll.DeleteSupplier(id);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }
    }
}
