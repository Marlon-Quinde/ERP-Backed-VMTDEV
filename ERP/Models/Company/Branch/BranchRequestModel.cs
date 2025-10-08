namespace ERP.Models.Company.Branch
{
    public class BranchRequestModel
    {
        public string? BranchRuc { get; set; }

        public string? BranchName { get; set; }

        public string? BranchAddress { get; set; }

        public string? BranchPhone { get; set; }
        public int? CompanyId { get; set; }

        public string? CodEstablSri { get; set; }

    }
}
