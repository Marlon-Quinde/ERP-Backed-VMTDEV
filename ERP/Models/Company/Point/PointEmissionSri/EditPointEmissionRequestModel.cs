namespace ERP.Models.Company.Point.PointEmissionSri
{
    public class EditPointEmissionRequestModel
    {
        public string? PointEmision { get; set; }

        public int? CompanyId { get; set; }

        public int? BranchId { get; set; }

        public string? CodEstablishmentSri { get; set; }

        public int? UltSecuence { get; set; }
    }
}
