using ERP.CoreDB;
using ERP.Helper.Models;
using ERP.Models.PointOfIssue;

namespace ERP.Bll.PointOfIssueBll
{
    public interface IPointIssueBll
    {
        ResponseGeneralModel<string?> CrearPuntoEmision(PointIssueRequestModel request);
        public List<PuntoEmisionSri> GetPuntoEmisionSri();
    }
}
