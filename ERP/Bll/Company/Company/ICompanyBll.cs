using ERP.Helper.Models;
using ERP.Models.Company.Company;
using ERP.Models.Inventory.Company;
using DbEmpresa = ERP.CoreDB.Empresa;


namespace ERP.Bll.Company.Company
{
    public interface ICompanyBll
    {
        ResponseGeneralModel<string?> CreateCompany(CompanyRequestModel request);
        //public List<DbEmpresa> GetCompany();
        public object GetCompany();

        //ResponseGeneralModel<bool?> DeleteCompany(int id);
        ResponseGeneralModel<bool?> EditCompany(int id, EditCompanyRequestModel requestModel);

    }
}
