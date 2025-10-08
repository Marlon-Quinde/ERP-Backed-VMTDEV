namespace ERP.Models.Invoice.MovemetDetProduct
{
    public class MovemetDetProductResponseModel
    {
        public int MovdetProdId { get; set; }

        public int? MovcabId { get; set; }

        public int? ProductId { get; set; }

        public int? Amount { get; set; }

        public decimal? Price { get; set; }
        public short? State { get; set; }

        public DateTime? DatetimeReg { get; set; }

        public DateTime? DatetimeAct { get; set; }

        public int? UsuIdReg { get; set; }

        public int? UsuIdAct { get; set; }
    }
}
