using ERP.CoreDB;
using ERP.Helper.Models;
using ERP.Models.Security.Authentication;

namespace ERP.Bll.Empresa
{
    public interface IClientBll
    {
        ResponseGeneralModel<string?> CreateClient(ClientRequestModel request);
        public List<Cliente> GetCliente();


    }
}
