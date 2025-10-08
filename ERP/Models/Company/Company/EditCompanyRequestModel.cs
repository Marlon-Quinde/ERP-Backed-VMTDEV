namespace ERP.Models.Company.Company
{
    public class EditCompanyRequestModel
    {
        public string? CompanyRuc { get; set; }

        public string? CompanyName { get; set; }

        public string? CompanyReason { get; set; }

        public string? CompanyAddressMatrix { get; set; }

        public string? CompanyPhoneMatrix { get; set; }

        public int? CityId { get; set; }
    }
}
