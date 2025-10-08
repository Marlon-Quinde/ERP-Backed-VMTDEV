namespace ERP.Models.Invoice.MovemetDetPay
{
    public class MovemetDetPayRequestModel
    {
        public int MovidetPayId { get; set; }

        public int? MovicabId { get; set; }

        public int? FormPayId { get; set; }

        public decimal? ValuePaid { get; set; }

        public int? IndustryId { get; set; }

        public string? Batch { get; set; }

        public string? Voucher { get; set; }

        public int? CreditCardId { get; set; }

        public int? BankId { get; set; }

        public int? ComprbnId { get; set; }

        public string? DatePay { get; set; }

        public int? CustomerId { get; set; }
    }
}
