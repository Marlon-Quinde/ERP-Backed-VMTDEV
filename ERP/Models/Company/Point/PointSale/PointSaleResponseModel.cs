namespace ERP.Models.Company.Point.PointSale
{
    public class PointSaleResponseModel
    {
        public int PointSaleId { get; set; }

        public string? PointSaleName { get; set; }

        public int? PointEmissionId { get; set; }      
        public int? SucursalId { get; set; }
        public short? State { get; set; }

        public DateTime? DatetimeReg { get; set; }

        public DateTime? DatetimeAct { get; set; }

        public int? UsuIdReg { get; set; }

        public int? UsuIdAct { get; set; }
    }
}
