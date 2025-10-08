namespace ERP.Models.Company.Branch
{
    public class BranchResponseModel
    {
        public int BranchId { get; set; }

        public string? BranchRuc { get; set; }

        public string? BranchName { get; set; }

        public string? BranchAddress { get; set; }

        public string? BranchPhone { get; set; }
        public int? CompanyId { get; set; }

        public string? CodEstablSri { get; set; }

        public short? State { get; set; }

        public DateTime? DateTimeReg { get; set; }
        public DateTime? DateTimeAct { get; set; }
        public int? UsuIdReg { get; set; }
        public int? UsuIdAct { get; set; }

    }
}
