namespace ERP.Models.Products.Stock
{
    public class StockRequestModel
    {
        public int? CompanyId { get; set; }

        public int? BranchId { get; set; }

        public int? WarehouseId { get; set; }

        public int? ProdId { get; set; }

        public int? AmountStock { get; set; }
    }
}
