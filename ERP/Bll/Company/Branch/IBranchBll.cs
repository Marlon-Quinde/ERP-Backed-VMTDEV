using ERP.CoreDB;
using ERP.Helper.Models;
using ERP.Models.Company.Branch;

namespace ERP.Bll.Company.Branch
{
    public interface IBranchBll
    {
        ResponseGeneralModel<string?> CreateBranch(BranchRequestModel request);
        ResponseGeneralModel<bool?> EditBranch(int id, EditBranchRequestModel requestModel);
        //ResponseGeneralModel<bool?> DeleteBranch(int id);
        //public List<Sucursal> GetBranches();
        public object GetBranches();

    }
}
