using ERP.CoreDB;
using ERP.Helper.Models;
using ERP.Models.Products.Brand;

namespace ERP.Bll.Products.Brand
{
    public interface IBrandBll
    {
        ResponseGeneralModel<bool?> EditBrand(int id, BrandRequestModel requestModel);
        //public List<Marca> GetBrand();
        public object GetBrand();

        ResponseGeneralModel<string?> CreateBrand(BrandRequestModel request);
        ResponseGeneralModel<bool?> DeleteBrand(int id);
    }
}
