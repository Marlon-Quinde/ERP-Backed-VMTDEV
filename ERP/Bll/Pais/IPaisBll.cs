using ERP.Helper.Models;
using ERP.Models.test;

namespace ERP.Bll.Location
{
    public interface IPaisBll
    {
        ResponseGeneralModel<string?> CrearPaisYCiudad(TestDbCommitRequestModel request);
    }
}
