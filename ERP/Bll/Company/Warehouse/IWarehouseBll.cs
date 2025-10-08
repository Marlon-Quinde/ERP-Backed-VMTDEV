using ERP.CoreDB;
using ERP.Helper.Models;
using ERP.Models.Company.Warehouse;
using ERP.Models.Inventory.Warehouse;

namespace ERP.Bll.Company.Warehouse
{
    public interface IWarehouseBll
    {
        ResponseGeneralModel<string?> CreateWarehouse(WarehouseRequestModel request);
        ResponseGeneralModel<bool?> EditWarehouse(int id, EditWarehouseRequestModel requestModel);
        ResponseGeneralModel<bool?> DeleteWarehouse(int id);
        //public List<Bodega> GetWarehouse();
        public object GetWarehouse();

    }
}
