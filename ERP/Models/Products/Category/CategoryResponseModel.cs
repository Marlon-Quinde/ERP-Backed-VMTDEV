namespace ERP.Models.Products.Category
{
    public class CategoryResponseModel
    {
        public int CategoryId { get; set; }

        public string? CategoryDescrip { get; set; }

        public short? State { get; set; }

        public DateTime? DatetimeReg { get; set; }

        public DateTime? DatetimeAct { get; set; }

        public int? UsuIdReg { get; set; }

        public int? UsuIdAct { get; set; }
    }
}
