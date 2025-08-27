using ERP.Helper.Models;
using ERP.Models.Invoice;

namespace ERP.Bll.Invoice
{
    public interface IInvoiceBll
    {
        Task<ResponseGeneralModel<string?>> CreateInvoice(InvoiceRequestModel value);
    }
}
