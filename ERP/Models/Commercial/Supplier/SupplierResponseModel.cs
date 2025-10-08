namespace ERP.Models.Commercial.Supplier
{
    public class SupplierResponseModel
    {
        public int SupplierId { get; set; }

        public string? SupplierRuc { get; set; }

        public string? SupplierNameCommercial { get; set; }

        public string? SupplierReason { get; set; }

        public string? SupplierAddress { get; set; }

        public int? SupplierPhone { get; set; }

        public int? CityId { get; set; }
        public short? State { get; set; }

        public DateTime? DatetimeReg { get; set; }

        public DateTime? DatetimeAct { get; set; }

        public int? UsuIdReg { get; set; }

        public int? UsuIdAct { get; set; }
    }
}
