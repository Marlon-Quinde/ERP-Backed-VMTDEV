using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Helper;
using ERP.Helper.Models;
using ERP.Models.Company.Company;
using ERP.Models.Company.Point.PointEmissionSri;
using ERP.Models.Company.Point.PointSale;
using ERP.Models.Inventory.Company;
using ERP.Models.Inventory.Warehouse;
using Newtonsoft.Json;

namespace ERP.Bll.Company.Point
{
    public class PointBll : IPointBll
    {
        private readonly BaseErpContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private SessionModel sessMod;

        public PointBll(BaseErpContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;

            string token = _httpContext.HttpContext.Request.Headers["token"];
            if (string.IsNullOrEmpty(token))
                throw new UnauthorizedAccessException("Token requerido");

            MethodsHelper<object> metHel = new MethodsHelper<object>();
            var decToken = metHel.DecodeJwtSession(token);

            if (decToken != null && decToken.code == 200)
                sessMod = JsonConvert.DeserializeObject<SessionModel>(decToken.message);
            else
                throw new UnauthorizedAccessException("Token inválido o expirado");
        }

        // Métodos de Punto de Emisión 
        public ResponseGeneralModel<string?> CreatePointEmission(PointEmissionRequestModel request)
        {
            try
            {
                _context.Database.BeginTransaction();

                var pointEmissionExist = _context.PuntoEmisionSris
                    .FirstOrDefault(p => p.PuntoEmision.ToUpper() == request.PointEmision.ToUpper()
                    && p.EmpresaId == request.CompanyId
                                         && p.SucursalId == request.BranchId);

                if (pointEmissionExist == null)
                {
                    var lastPointEmission = _context.PuntoEmisionSris
                        .OrderByDescending(p => p.PuntoEmisionId)
                        .FirstOrDefault();

                    int newPointEmissionId = lastPointEmission == null ? 1 : lastPointEmission.PuntoEmisionId + 1;

                    pointEmissionExist = new PuntoEmisionSri
                    {
                        PuntoEmisionId = newPointEmissionId,
                        PuntoEmision = request.PointEmision,
                        EmpresaId = request.CompanyId ?? 0,
                        SucursalId = request.BranchId ?? 0,
                        CodEstablecimientoSri = request.CodEstablishmentSri,
                        UltSecuencia = request.UltSecuence ?? 0,
                        Estado = 1,
                        FechaHoraReg = DateTime.Now,
                        UsuIdReg = sessMod.id
                    };

                    _context.PuntoEmisionSris.Add(pointEmissionExist);
                    _context.SaveChanges();
                }

                _context.Database.CommitTransaction();
                return new ResponseGeneralModel<string?>(200, MessageHelper.pointSaleCorrect);
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.pointSaleIncorrect, ex.ToString());
            }
        }
        public ResponseGeneralModel<bool?> EditPointEmission(int id, EditPointEmissionRequestModel requestModel)
        {
            var pointEmission = _context.PuntoEmisionSris.FirstOrDefault(item => item.PuntoEmisionId == id);
            if (pointEmission == null)
                return new ResponseGeneralModel<bool?>(404, null, "Punto de emisión no encontrado");
            var companyExists = _context.Empresas.Any(e => e.EmpresaId == requestModel.CompanyId);
            if (!companyExists)
                return new ResponseGeneralModel<bool?>(400, null, "La empresa especificada no existe");
            var branchExists = _context.Sucursals.Any(s => s.SucursalId == requestModel.BranchId);
            if (!branchExists)
                return new ResponseGeneralModel<bool?>(400, null, "La sucursal especificada no existe");
            pointEmission.PuntoEmision = requestModel.PointEmision;
            pointEmission.CodEstablecimientoSri = requestModel.CodEstablishmentSri;
            pointEmission.EmpresaId = requestModel.CompanyId ?? 0;
            pointEmission.SucursalId = requestModel.BranchId ?? 0;
            pointEmission.UltSecuencia = requestModel.UltSecuence ?? pointEmission.UltSecuencia;
            pointEmission.UsuIdAct = sessMod.id;
            pointEmission.FechaHoraAct = DateTime.Now;

            _context.SaveChanges();

            return new ResponseGeneralModel<bool?>(200, MessageHelper.pointEmissionEditCorrect);
        }


        public  object GetPuntoEmisionSri()
        {
            return _context.PuntoEmisionSris
                .Select(c => new
                {
                    c.PuntoEmisionId,
                    c.PuntoEmision,
                    c.EmpresaId,
                    c.SucursalId,
                    c.CodEstablecimientoSri,
                    c.UltSecuencia,
                    EmpresaNombre = c.Empresa.EmpresaNombre,
                    SucursalNombre = c.Sucursal.SucursalNombre,
                    PuntoVenta = c.PuntoVentum == null ? null : new
                    {
                        c.PuntoVentum.PuntovtaId,
                        c.PuntoVentum.PuntovtaNombre
                    },
                    Estado = c.Estado == null ? 0 : c.Estado,
                    c.FechaHoraReg,
                    c.FechaHoraAct,
                    c.UsuIdReg,
                    c.UsuIdAct
                })
                .ToList();
        }

        // Punto de Venta
        public ResponseGeneralModel<string?> CreatePointSale(PointSaleRequestModel request)
        {
            try
            {
                var pointEmission = _context.PuntoEmisionSris
                    .FirstOrDefault(p => p.PuntoEmisionId == request.PointEmissionId);
                if (pointEmission == null)
                    return new ResponseGeneralModel<string?>(400, null, "El punto de emisión especificado no existe");
                var pointSaleExist = _context.PuntoVenta
                    .FirstOrDefault(p => p.PuntovtaNombre!.ToUpper() == request.PointSaleName.ToUpper());

                if (pointSaleExist == null)
                {
                    var newPointSale = new PuntoVentum
                    {
                        PuntovtaNombre = request.PointSaleName,
                        PuntoEmisionId = request.PointEmissionId,
                        Estado = 1,
                        FechaHoraReg = DateTime.Now,
                        UsuIdReg = sessMod.id
                    };

                    _context.PuntoVenta.Add(newPointSale);
                    _context.SaveChanges();
                }

                return new ResponseGeneralModel<string?>(200, MessageHelper.pointSaleCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.pointSaleIncorrect, ex.ToString());
            }
        }

        public ResponseGeneralModel<bool?> EditPointSale(int id, EditPointSaleRequestModel requestModel)
        {
            var pointSale = _context.PuntoVenta.FirstOrDefault(item => item.PuntovtaId == id);
            if (pointSale == null)
                return new ResponseGeneralModel<bool?>(404, null, "Punto de venta no encontrado");
            var pointEmissionExists = _context.PuntoEmisionSris.Any(p => p.PuntoEmisionId == requestModel.PointEmissionId);
            if (!pointEmissionExists)
                return new ResponseGeneralModel<bool?>(400, null, "El punto de emisión especificado no existe");

            pointSale.PuntovtaNombre = requestModel.PointSaleName;
            pointSale.PuntoEmisionId = requestModel.PointEmissionId ?? pointSale.PuntoEmisionId;
            pointSale.UsuIdAct = sessMod.id;
            pointSale.FechaHoraAct = DateTime.Now;

            _context.SaveChanges();

            return new ResponseGeneralModel<bool?>(200, MessageHelper.pointSaleEditCorrect);
        }

        public object GetPuntoVenta()
        {
            return _context.PuntoVenta.Select(
                c=> new
                {
                    c.PuntovtaId,
                    c.PuntovtaNombre,
                    c.PuntoEmisionId,
                    Estado = c.Estado == null ? 0 : c.Estado,
                    c.FechaHoraReg,
                    c.FechaHoraAct,
                    c.UsuIdReg,
                    c.UsuIdAct
                }).ToList();
        }
    }

}
