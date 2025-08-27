namespace ERP.Models.PointOfSale
{
    public class PointSaleResponseModel
    {
        public int PointSaleId { get; set; }
        public string PointSaleName { get; set; } = string.Empty;
        public int Status { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}
