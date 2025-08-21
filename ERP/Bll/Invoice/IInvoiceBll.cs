namespace ERP.Bll.Invoice
{
    public interface IInvoiceBll
    {
        public Task<string> CreateInvoice(string value);
    }
}
