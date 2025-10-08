namespace ERP.Models.Inventory.Warehouse
{
    public class WarehouseResponseModel
    {
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public string WarehouseAddress { get; set; }
        public string WarehousePhone { get; set; }
        public int? SucursalId { get; set; }

        public short State { get; set; }
        public DateTime? DateTimeReg { get; set; }

        public DateTime? DameTimeAct { get; set; }

        public int? UsuIdReg { get; set; }

        public int? UsuIdAct { get; set; }

    }
}
