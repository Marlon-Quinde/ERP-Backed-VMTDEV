namespace ERP.Models.Products.Stock
{
    public class StockResponseModel
    {
        public long StockId { get; set; }

        public int? CompanyId { get; set; }

        public int? BranchId { get; set; }

        public int? WarehouseId { get; set; }

        public int? ProdId { get; set; }

        public int? AmountStock { get; set; }

        public short? State { get; set; }

        public DateTime? DatetimeReg { get; set; }

        public DateTime? DatetimeAct { get; set; }

        public int? UsuIdReg { get; set; }

        public int? UsuIdAct { get; set; }
    }
}
