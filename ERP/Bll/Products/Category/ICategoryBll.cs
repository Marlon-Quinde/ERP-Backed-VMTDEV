using ERP.CoreDB;
using ERP.Helper.Models;
using ERP.Models.Products.Category;

namespace ERP.Bll.Products.Category
{
    public interface ICategoryBll
    {
        ResponseGeneralModel<string?> CreateCategory(CategoryRequestModel request);
        ResponseGeneralModel<bool?> EditCategory(int id, CategoryRequestModel requestModel);
        ResponseGeneralModel<bool?> DeleteCategory(int id);
        //public List<Categorium> GetCategory();
        public object GetCategory();


    }
}
