using ERP.CoreDB;
using ERP.Helper.Models;
using ERP.Models.Invoice.MovemetCab;
using ERP.Models.Invoice.MovemetDetPay;
using ERP.Models.Invoice.MovemetDetProduct;

namespace ERP.Bll.Invoice.MovementInvoice
{
    public interface IMovementInvoiceBll
    {
        ResponseGeneralModel<string?> CreateMovemetCab(MovemetCabRequestModel request);
        ResponseGeneralModel<bool?> EditMovementCab(int id, MovemetCabRequestModel request);
        //public List<MovimientoCab> GetMovementCab();
        public object GetMovementCab();

        ResponseGeneralModel<string?> CreateMovementDetPay(MovemetDetPayRequestModel request);
        ResponseGeneralModel<bool?> EditMovementDetPay(int id, MovemetDetPayRequestModel request);
        //public List<MovimientoDetPago> GetMovementDetPay();
        public object GetMovementDetPay();

        ResponseGeneralModel<string?> CreateMovemetDetProduct(MovemetDetProductRequestModel request);
        ResponseGeneralModel<bool?> EditMovementDetProduct(int id, MovemetDetProductRequestModel request);
        //public List<MovimientoDetProducto> GetMovementDetProduct();
        public object GetMovementDetProduct();


    }
}
