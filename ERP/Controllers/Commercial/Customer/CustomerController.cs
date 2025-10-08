using ERP.Bll.Commercial.Customer;
using ERP.Bll.Company.Company;
using ERP.Bll.Company.Warehouse;
using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Models;
using ERP.Models.Commercial.Customer;
using ERP.Models.Company.Warehouse;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP.Controllers.Commercial.Customer
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerBll customerBll;

        public CustomerController(ICustomerBll bll)
        {
            customerBll = bll;
        }

        [HttpGet("listar-Cliente")]
        public ResponseGeneralModel<object?> TestDb()
        {
            try
            {
                return new ResponseGeneralModel<object?>(200, customerBll.GetCustomer(), MessageHelper.CustomerCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<object?>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }


        [HttpPost("Crear-Cliente")]
        public ResponseGeneralModel<string?> Post([FromBody] CustomerRequestModel requestModel)
        {
            try { 
            
                ResponseGeneralModel<string?> isOk = customerBll.CreateCustomer(requestModel);
                return isOk;
            }
            catch
            {
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.errorGeneral);
            }
        }

        [HttpPut("Actualizar-Cliente")]
        public ResponseGeneralModel<bool?> Put(int id, [FromBody] EditCustomerRequestModel requestModel)
        {
            try
            {
                return customerBll.EditCustomer(id, requestModel);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }
        [HttpDelete("{id}")]
        public ResponseGeneralModel<bool?> DeleteCustomer(int id)
        {
            try
            {
                return customerBll.DeleteCustomer(id);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }
    }
}
