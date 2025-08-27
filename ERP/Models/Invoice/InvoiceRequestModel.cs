namespace ERP.Models.Invoice
{
    public class InvoiceRequestModel
    {
        public List<InvoiceRequestModel_products> products { get; set; }
    }


    public class InvoiceRequestModel_products
    {
        public int productId { get; set; }
        public int qty { get; set; }
    }
}
