using ERP.Helper.Models;
using ERP.Models.Products.Stock;

namespace ERP.Bll.Products.Stock
{
    public interface IStockBll
    {
        ResponseGeneralModel<string?> CreateStock(StockRequestModel request);
        ResponseGeneralModel<bool?> EditStock(int id, StockRequestModel request);
        public object GetAllStock();

    }
}
