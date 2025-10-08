using ERP.CoreDB;
using ERP.Helper.Models;
using ERP.Models.Products.Product;

namespace ERP.Bll.Products.Product
{
    public interface IProductBll
    {
        //public List<Producto> GetProduct();

        public object GetProduct();

        ResponseGeneralModel<bool?> DeleteProduct(int id);
        ResponseGeneralModel<bool?> EditProduct(int id, ProductRequestModel requestModel);
        ResponseGeneralModel<string?> CreateProduct(ProductRequestModel request);
    }
}
