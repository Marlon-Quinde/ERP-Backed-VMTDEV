namespace ERP.Models.Invoice.MovemetDetProduct
{
    public class MovemetDetProductRequestModel
    {
        public int MovdetProdId { get; set; }

        public int? MovcabId { get; set; }

        public int? ProductId { get; set; }

        public int? Amount { get; set; }

        public decimal? Price { get; set; }
    }
}
