namespace ERP.Models.Invoice.MovemetCab
{
    public class MovemetCabResponseModel
    {
        public int MovicabId { get; set; }

        public int? TypeMovId { get; set; }

        public int? TypeMovIngEgr { get; set; }

        public int? CompanyId { get; set; }

        public int? BranchId { get; set; }

        public int? WarehouseId { get; set; }

        public string? SecuenceInvoice { get; set; }

        public string? AutorizationSri { get; set; }

        public string? AccessKey { get; set; }

        public int? CustomerId { get; set; }

        public int? PointSaleId { get; set; }

        public int? SupplierId { get; set; }

        public string? State { get; set; }

        public DateTime? DatetimeReg { get; set; }

        public DateTime? DatetimeAct { get; set; }

        public int? UsuIdReg { get; set; }

        public int? UsuIdAct { get; set; }
    }
}
