using ERP.CoreDB;
using ERP.Helper.Models;
using ERP.Models.Products.Tax;

namespace ERP.Bll.Products.Tax
{
    public interface ITaxBll
    {
        ResponseGeneralModel<bool?> EditTax(int id, TaxRequestModel requestModel);
        ResponseGeneralModel<string?> CreateTax(TaxRequestModel request);
        //public List<Impuesto> GetTax();
        public object GetTax();


    }
}
