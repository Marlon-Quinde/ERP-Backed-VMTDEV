namespace ERP.Models.Company.Point.PointEmissionSri
{
    public class PointEmissionResponseModel
    {
        public int PointEmisionId { get; set; }

        public string? PointEmision { get; set; }

        public int? CompanyId { get; set; }

        public int? BranchId { get; set; }

        public string? CodEstablishmentSri { get; set; }

        public int? UltSecuence { get; set; }

        public short? State { get; set; }

        public DateTime? DatetimeReg { get; set; }

        public DateTime? DatetimeAct { get; set; }

        public int? UsuIdReg { get; set; }

        public int? UsuIdAct { get; set; }
    }
}
