using ERP.CoreDB;
using ERP.Helper.Models;
using ERP.Models.Commercial.Supplier;

namespace ERP.Bll.Commercial.Supplier
{
    public interface ISupplierBll
    {
        ResponseGeneralModel<string?> CreateSupplier(SupplierRequestModel request);
        ResponseGeneralModel<bool?> EditSupplier(int id, EditSupplierRequestModel requestModel);
        ResponseGeneralModel<bool?> DeleteSupplier(int id);
        //public List<Proveedor> GetSupplier();
        public object GetSupplier();


    }
}
