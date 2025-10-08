using ERP.CoreDB;

namespace ERP.Models.Products.Product
{
    public class ProductRequestModel
    {
        public string? ProdDescription { get; set; }

        public decimal? ProdUltPrice { get; set; }

        public int? CategoryId { get; set; }

        public int? CompanyId { get; set; }

        public int? SupplierId { get; set; }

        public int? BrandId { get; set; }


    }
}
