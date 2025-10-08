using ERP.Bll.Company.Branch;
using ERP.Bll.Company.Company;
using ERP.Helper.Data;
using ERP.Helper.Models;
using ERP.Models.Company.Branch;
using ERP.Models.Company.Company;
using ERP.Models.Inventory.Company;
using Microsoft.AspNetCore.Mvc;
using DbEmpresa = ERP.CoreDB.Empresa;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP.Controllers.Commercial.Company
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyBll companyBll;

        public CompanyController(ICompanyBll bll)
        {
            companyBll = bll;
        }

        [HttpGet("listar")]
        public ResponseGeneralModel<object?> GetCompany()
        {
            try
            {
                return new ResponseGeneralModel<object?>(200, companyBll.GetCompany(), MessageHelper.CompanyCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<object?>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }

        [HttpPost("Crear")]
        public ResponseGeneralModel<string?> CreateCompany([FromBody] CompanyRequestModel request)
        {
            return companyBll.CreateCompany(request);
        }

        [HttpPut("Actualizar")]
        public ResponseGeneralModel<bool?> Put(int id, [FromBody] EditCompanyRequestModel requestModel)
        {
            try
            {
                return companyBll.EditCompany(id, requestModel);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }
    }
}
