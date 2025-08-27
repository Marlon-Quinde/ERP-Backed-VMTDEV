using ERP.CoreDB;
using ERP.Helper.Models;
using ERP.Models.PointOfSale;

namespace ERP.Bll.PointSaleBll
{
    public interface IPointSaleBll
    {
        ResponseGeneralModel<string?> CreatePointSale(PointSaleRequestModel request);
        ResponseGeneralModel<List<PointSaleResponseModel>> GetPointSales();
    }
}
