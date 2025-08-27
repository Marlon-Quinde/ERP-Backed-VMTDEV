namespace ERP.Helper.Models.Views.Pdf
{
    public class ElectronicInvoiceViewPdf
    {
        public string namePerson { get; set; }
        public List<ElectronicInvoiceViewPdf_products> products { get; set; }
        public decimal total { get; set; }
    }


    public class ElectronicInvoiceViewPdf_products
    {
        public ElectronicInvoiceViewPdf_products(int qty, string name, decimal price, decimal tot)
        {
            this.qty = qty;
            this.name = name;
            this.price = price;
            this.tot = tot;
        }

        public int qty { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public decimal tot { get; set; }
    }
}
