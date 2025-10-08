using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Helper;
using ERP.Helper.Models;
using ERP.Models.Commercial.Supplier;
using Newtonsoft.Json;
using System.Data.Entity;

namespace ERP.Bll.Commercial.Supplier
{
    public class SupplierBll : ISupplierBll
    {
        private readonly BaseErpContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private SessionModel sessMod;

        public SupplierBll(BaseErpContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;

            string token = _httpContext.HttpContext.Request.Headers["token"];
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token requerido");
            }

            MethodsHelper<SupplierRequestModel> metHel = new MethodsHelper<SupplierRequestModel>();
            ResponseGeneralModel<SupplierRequestModel> decToken = metHel.DecodeJwtSession(token);

            if (decToken != null && decToken.code == 200)
            {
                sessMod = JsonConvert.DeserializeObject<SessionModel>(decToken.message);
            }
            else
            {
                throw new UnauthorizedAccessException("Token inválido o expirado");
            }
        }

        public ResponseGeneralModel<string?> CreateSupplier(SupplierRequestModel request)
        {
            try
            {
                _context.Database.BeginTransaction();
                var SupplierExist = _context.Proveedors
                    .FirstOrDefault(p => p.ProvNomComercial.ToUpper() == request.SupplierNameCommercial.ToUpper());
                var lastSupplier = _context.Proveedors
                    .OrderByDescending(p => p.ProvId)
                    .FirstOrDefault();

                int newSupplierId = lastSupplier == null ? 1 : lastSupplier.ProvId + 1;
                if (SupplierExist == null)
                {
                    var LastSupplier = _context.Proveedors.OrderByDescending(p => p.ProvId).FirstOrDefault();
                    SupplierExist = new Proveedor
                    {
                        ProvId = newSupplierId,
                        ProvRuc = request.SupplierRuc,
                        ProvNomComercial = request.SupplierNameCommercial,
                        ProvRazon = request.SupplierReason,
                        ProvDireccion = request.SupplierAddress,
                        ProvTelefono = request.SupplierPhone,
                        CiudadId = request.CityId,
                        UsuIdReg = sessMod.id,
                        Estado = "1",
                        FechaHoraReg = DateTime.Now
                    };
                    _context.Proveedors.Add(SupplierExist);
                    _context.SaveChanges();
                }

                _context.Database.CommitTransaction();

                return new ResponseGeneralModel<string?>(200, MessageHelper.SupplierCorrect);
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.SupplierError, ex.ToString());
            }
        }
        public ResponseGeneralModel<bool?> EditSupplier(int id, EditSupplierRequestModel requestModel)
        {
            Proveedor supplier = _context.Proveedors.First((item) => item.ProvId == id);

            supplier.ProvRuc = requestModel.SupplierRuc;
            supplier.ProvNomComercial = requestModel.SupplierNameCommercial;
            supplier.ProvRazon = requestModel.SupplierReason;
            supplier.ProvDireccion = requestModel.SupplierAddress;
            supplier.ProvTelefono = requestModel.SupplierPhone;
            supplier.CiudadId = requestModel.CityId;
            supplier.UsuIdAct = sessMod.id;
            supplier.FechaHoraAct = DateTime.Now;
            _context.SaveChanges();

            return new ResponseGeneralModel<bool?>(200, MessageHelper.SupplierEdit);
        }

        public ResponseGeneralModel<bool?> DeleteSupplier(int id)
        {
            try
            {
                Proveedor? supplier = _context.Proveedors.FirstOrDefault(r => r.ProvId == id);
                if (supplier == null)
                {
                    return new ResponseGeneralModel<bool?>(404, null, MessageHelper.SupplierNotFound);
                }
                supplier.Estado = "0";
                supplier.UsuIdAct = sessMod.id;
                supplier.FechaHoraAct = DateTime.Now;
                _context.SaveChanges();

                return new ResponseGeneralModel<bool?>(200, true, MessageHelper.SupplierDelete);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.SupplierErrorDelete, ex.ToString());
            }
        }
        public object GetSupplier()
        {
            return _context.Proveedors
                .Include(s => s.MovimientoCabs)
                .Select(s => new
                {
                    s.ProvId,
                    s.ProvRuc,
                    s.ProvNomComercial,
                    s.ProvRazon,
                    s.ProvDireccion,
                    s.ProvTelefono,
                    s.CiudadId,
                    Estado = s.Estado ?? "0",
                    s.FechaHoraReg,
                    s.FechaHoraAct,
                    s.UsuIdReg,
                    s.UsuIdAct,
                    MovimientoCabs = s.MovimientoCabs.Select(m => new
                    {
                        m.MovicabId,
                        m.SecuenciaFactura,
                        m.FechaHoraReg,
                        m.ClienteId,
                        m.ProveedorId
                    }).ToList()
                })
                .ToList();
        }

    }
}
