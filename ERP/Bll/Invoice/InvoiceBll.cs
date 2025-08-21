using ERP.Helper.Helper.TemplateView;
using ERP.Helper.Models.Views.Pdf;

namespace ERP.Bll.Invoice
{
    public class InvoiceBll : IInvoiceBll
    {

        private readonly ITemplateViewHelper _templateViewHelper;

        public InvoiceBll(ITemplateViewHelper templateViewHelper)
        {
            _templateViewHelper = templateViewHelper;
        }

        public Task<string> CreateInvoice(string value)
        {
            return _templateViewHelper.RenderViewToStringAsync("Pdf/ElectronicInvoice", new ElectronicInvoiceViewPdf()
            {
                name = value,
            });
        }
    }
}
