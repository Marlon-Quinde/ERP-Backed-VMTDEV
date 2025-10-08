namespace ERP.Models.Products.Brand
{
    public class BrandResponseModel
    {
        public int BrandhId { get; set; }

        public string? BrandDescrip { get; set; }

        public short? State { get; set; }

        public DateTime? DatetimeReg { get; set; }

        public DateTime? DatetimeAct { get; set; }

        public int? UsuIdReg { get; set; }

        public int? UsuIdAct { get; set; }

    }
}
