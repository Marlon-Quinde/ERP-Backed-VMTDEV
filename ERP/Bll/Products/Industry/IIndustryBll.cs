using ERP.CoreDB;
using ERP.Helper.Models;
using ERP.Models.Products.Industry;

namespace ERP.Bll.Products.Industry
{
    public interface IIndustryBll
    {
        ResponseGeneralModel<string?> CreateIndustry(IndustryRequestModel request);
        ResponseGeneralModel<bool?> EditIndustry(int id, IndustryRequestModel requestModel);
        //public List<Industrium> GetIndustry();
        public object GetIndustry();


    }
}
