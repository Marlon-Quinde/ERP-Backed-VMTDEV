using ERP.CoreDB;
using ERP.Helper.Models;
using ERP.Models.Commercial.Customer;

namespace ERP.Bll.Commercial.Customer
{
    public interface ICustomerBll
    {
        ResponseGeneralModel<string?> CreateCustomer(CustomerRequestModel request);
        ResponseGeneralModel<bool?> EditCustomer(int id, EditCustomerRequestModel requestModel);
        ResponseGeneralModel<bool?> DeleteCustomer(int id);
        //public List<Cliente> GetCustomer();
        public object GetCustomer();

    }
}
