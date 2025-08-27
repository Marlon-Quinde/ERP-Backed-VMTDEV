namespace ERP.Models.PointOfSale
{
    public class PointSaleRequestModel
    {
        public string PointSaleName { get; set; } = string.Empty;
        public int PointEmissionId { get; set; }
        public int SucursalId { get; set; }
        public int UserId { get; set; }
        public int PuntoEmisionId { get; internal set; }

    }
}
