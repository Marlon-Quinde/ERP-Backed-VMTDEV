namespace ERP.Models.Products.Industry
{
    public class IndustryResponseModel
    {
        public int IndustryId { get; set; }

        public string? IndustryDescription { get; set; }

        public short? State { get; set; }

        public DateTime? DatetimeReg { get; set; }

        public DateTime? DatetimeAct { get; set; }

        public int? UsuIdReg { get; set; }

        public int? UsuIdAct { get; set; }
    }
}
