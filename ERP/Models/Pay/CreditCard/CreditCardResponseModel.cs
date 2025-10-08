namespace ERP.Models.Pay.CreditCard
{
    public class CreditCardResponseModel
    {
        public int CreditCardId { get; set; }

        public string? CreditCardDescription { get; set; }

        public int? IndustryId { get; set; }

        public DateTime? DatetimeReg { get; set; }

        public DateTime? DatetimeAct { get; set; }

        public int? UsuIdReg { get; set; }

        public int? UsuIdAct { get; set; }
    }
}
