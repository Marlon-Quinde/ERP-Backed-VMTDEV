namespace ERP.Models.Commercial.Customer
{
    public class CustomerResponseModel
    {
        public int CustomerId { get; set; }

        public string? CustomerRuc { get; set; }

        public string? CustomerName1 { get; set; }

        public string? CustomerName2 { get; set; }

        public string? CustomerLastName1 { get; set; }

        public string? CustomerLastName2 { get; set; }

        public string? CustomerEmail { get; set; }

        public string? CustomerPhone { get; set; }

        public string? CustomerAddress { get; set; }

        public short? State { get; set; }

        public DateTime? DatetimeReg { get; set; }

        public DateTime? DatetimeAct { get; set; }

        public int? UsuIdReg { get; set; }

        public int? UsuIdAct { get; set; }
    }
}
