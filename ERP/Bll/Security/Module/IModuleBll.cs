using ERP.CoreDB;
using ERP.Helper.Models;
using ERP.Models.Pay.FormPay;
using ERP.Models.Security.Module;

namespace ERP.Bll.Security.Module
{
    public interface IModuleBll
    {
        ResponseGeneralModel<string?> CreateModule(ModuleRequestModel request);
        ResponseGeneralModel<bool?> EditModule(int id, ModuleRequestModel requestModel);
        public List<Modulo> GetModule();

    }
}
