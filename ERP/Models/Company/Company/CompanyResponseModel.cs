namespace ERP.Models.Inventory.Company
{
    public class CompanyResponseModel
    {
        public int CompanyId { get; set; }

        public string? CompanyName { get; set; }

        public string? CompanyReason { get; set; }

        public string? CompanyAddressMatrix { get; set; }

        public string? CompanyPhoneMatrix { get; set; }

        public int? CityId { get; set; }

        public short? State { get; set; }

        public DateTime? DateTimeReg { get; set; }

        public DateTime? DateTimeAct { get; set; }

        public int? UsuIdReg { get; set; }

        public int? UsuIdAct { get; set; }
    }
}
