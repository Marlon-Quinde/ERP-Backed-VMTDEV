namespace ERP.Models.Products.Tax
{
    public class TaxRequestModel
    {
        public short? TaxId { get; set; }

        public string? TaxDescr { get; set; }

        public decimal? TaxValue { get; set; }
    }
}
