using ERP.CoreDB;
using ERP.Helper.Models;
using ERP.Models.Company.Point.PointEmissionSri;
using ERP.Models.Company.Point.PointSale;

namespace ERP.Bll.Company.Point
{
    public interface IPointBll
    {
        ResponseGeneralModel<string?> CreatePointEmission(PointEmissionRequestModel request);
        ResponseGeneralModel<bool?> EditPointEmission(int id, EditPointEmissionRequestModel requestModel);
        //public List<PuntoEmisionSri> GetPuntoEmisionSri();
        public object GetPuntoEmisionSri();
        ResponseGeneralModel<string?> CreatePointSale(PointSaleRequestModel request);
        ResponseGeneralModel<bool?> EditPointSale(int id, EditPointSaleRequestModel requestModel);
        //public List<PuntoVentum> GetPuntoVenta();
        public object GetPuntoVenta();
    }
}
