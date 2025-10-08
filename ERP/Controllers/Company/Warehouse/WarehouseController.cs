using ERP.Bll.Company.Company;
using ERP.Bll.Company.Warehouse;
using ERP.Bll.Security.Role;
using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Models;
using ERP.Models.Company.Company;
using ERP.Models.Company.Warehouse;
using ERP.Models.Inventory.Company;
using ERP.Models.Inventory.Warehouse;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP.Controllers.Commercial.Warehouse
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseBll warehouseBll;

        public WarehouseController(IWarehouseBll bll)
        {
            warehouseBll = bll;
        }

        [HttpGet("listarBodega")]
        public ResponseGeneralModel<object?> GetWarehouse()
        {
            try
            {
                return new ResponseGeneralModel<object?>(200, warehouseBll.GetWarehouse(), MessageHelper.WarehouseCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<object?>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }

        [HttpPost("Crear-Bodega")]
        public ResponseGeneralModel<string?> CreateWarehouse([FromBody] WarehouseRequestModel request)
        {
            return warehouseBll.CreateWarehouse(request);
        }

        [HttpPut("Actualizar-Bodega")]
        public ResponseGeneralModel<bool?> Put(int id, [FromBody] EditWarehouseRequestModel requestModel)
        {
            try
            {
                return warehouseBll.EditWarehouse(id, requestModel);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }
        [HttpDelete("{id}")]
        public ResponseGeneralModel<bool?> DeleteWarehouse(int id)
        {
            try
            {
                return warehouseBll.DeleteWarehouse(id);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }
    }
}
