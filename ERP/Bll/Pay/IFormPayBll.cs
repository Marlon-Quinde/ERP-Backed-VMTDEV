using ERP.CoreDB;
using ERP.Helper.Models;
using ERP.Models.Pay.CreditCard;
using ERP.Models.Pay.FormPay;

namespace ERP.Bll.Pay
{
    public interface IFormPayBll
    {
        ResponseGeneralModel<string?> CreateCreditCard(CreditCardRequestModel request);
        ResponseGeneralModel<bool?> EditCreditCard(int id, CreditCardRequestModel requestModel);
        //public List<TarjetaCredito> GetCreditCard();
        public object GetCreditCard();

        //public List<FormaPago> GetFormPago();
        public object GetFormPago();

        ResponseGeneralModel<bool?> EditFormPago(int id, FormPayRequestModel requestModel);
        ResponseGeneralModel<string?> CreateFormPago(FormPayRequestModel request);

    }
}
