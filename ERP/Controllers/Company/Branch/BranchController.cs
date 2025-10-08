using ERP.Bll.Company.Branch;
using ERP.Bll.Company.Company;
using ERP.Bll.Security.Role;
using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Models;
using ERP.Models.Company.Branch;
using ERP.Models.Security.Role;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP.Controllers.Commercial.Branch
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchBll branchBll;

        public BranchController(IBranchBll bll)
        {
            branchBll = bll;
        }

        [HttpGet("listar")]
        public ResponseGeneralModel<object?> GetBranch()
        {
            try
            {
                return new ResponseGeneralModel<object?>(200, branchBll.GetBranches(), MessageHelper.BranchCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<object?>(500, null, MessageHelper.BranchIncorrect, ex.ToString());
            }
        }

        [HttpPost("Crear")]
        public ResponseGeneralModel<string?> CreateBranches([FromBody] BranchRequestModel request)
        {
            return branchBll.CreateBranch(request);
        }

        [HttpPut("Actualizar")]
        public ResponseGeneralModel<bool?> Put(int id, [FromBody] EditBranchRequestModel requestModel)
        {
            try
            {
                return branchBll.EditBranch(id, requestModel);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }
    }
}
