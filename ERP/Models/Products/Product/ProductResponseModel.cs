namespace ERP.Models.Products.Product
{
    public class ProductResponseModel
    {
        public int ProdId { get; set; }

        public string? ProdDescription { get; set; }

        public decimal? ProdUltPrice { get; set; }

        public int? CategoryId { get; set; }

        public int? CompanyId { get; set; }

        public int? SupplierId { get; set; }

        public int? BrandId { get; set; }

        public short? State { get; set; }

        public DateTime? DatetimeReg { get; set; }

        public DateTime? DatetimeAct { get; set; }

        public int? UsuIdReg { get; set; }

        public int? UsuIdAct { get; set; }
    }
}
