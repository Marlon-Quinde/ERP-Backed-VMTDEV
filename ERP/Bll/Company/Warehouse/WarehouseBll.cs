using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Helper;
using ERP.Helper.Models;
using ERP.Models.Company.Company;
using ERP.Models.Company.Warehouse;
using ERP.Models.Inventory.Warehouse;
using Newtonsoft.Json;

namespace ERP.Bll.Company.Warehouse
{
    public class WarehouseBll : IWarehouseBll
    {
        private readonly BaseErpContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private SessionModel sessMod;

        public WarehouseBll(BaseErpContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;

            string token = _httpContext.HttpContext.Request.Headers["token"];

            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token requerido");
            }

            MethodsHelper<WarehouseRequestModel> metHel = new MethodsHelper<WarehouseRequestModel>();
            ResponseGeneralModel<WarehouseRequestModel> decToken = metHel.DecodeJwtSession(token);

            if (decToken != null && decToken.code == 200)
            {
                sessMod = JsonConvert.DeserializeObject<SessionModel>(decToken.message);
            }
            else
            {
                throw new UnauthorizedAccessException("Token inválido o expirado");
            }
        }

        public ResponseGeneralModel<string?> CreateWarehouse(WarehouseRequestModel request)
        {
            try
            {
                _context.Database.BeginTransaction();

                var WarehouseExist = _context.Bodegas
                    .FirstOrDefault(p => p.BodegaNombre.ToUpper() == request.WarehouseName.ToUpper());

                if (WarehouseExist == null)
                {
                    var lastWarehouse = _context.Bodegas
                        .OrderByDescending(p => p.BodegaId)
                        .FirstOrDefault();

                    int newWarehouse = lastWarehouse == null ? 1 : lastWarehouse.BodegaId + 1;

                    WarehouseExist = new Bodega
                    {
                        BodegaNombre = request.WarehouseName,
                        BodegaDireccion = request.WarehouseAddress,
                        BodegaTelefono = request.WarehousePhone,
                        Estado = 1,
                        FechaHoraReg= DateTime.Now,
                        UsuIdAct = sessMod.id
                    };

                    _context.Bodegas.Add(WarehouseExist);
                    _context.SaveChanges();
                }

                _context.Database.CommitTransaction();

                return new ResponseGeneralModel<string?>(200, MessageHelper.WarehouseCorrect);
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.WarehouseIncorrect, ex.ToString());
            }
        }
        public ResponseGeneralModel<bool?> EditWarehouse(int id, EditWarehouseRequestModel requestModel)
        {
            Bodega warehouse = _context.Bodegas.First((item) => item.BodegaId == id);

            warehouse.BodegaNombre = requestModel.WarehouseName;
            warehouse.BodegaDireccion = requestModel.WarehouseAddress;
            warehouse.BodegaTelefono = requestModel.WarehousePhone;
            warehouse.UsuIdAct = sessMod.id;
            warehouse.FechaHoraAct = DateTime.Now;
            _context.SaveChanges();

            return new ResponseGeneralModel<bool?>(200, MessageHelper.WarehouseCorrect);
        }

        public ResponseGeneralModel<bool?> DeleteWarehouse(int id)
        {
            try
            {
                Bodega? warehouse = _context.Bodegas.FirstOrDefault(r => r.BodegaId == id);
                if (warehouse == null)
                {
                    return new ResponseGeneralModel<bool?>(404, null, MessageHelper.WarehouseNotFound);
                }
                warehouse.Estado = 0;
                warehouse.UsuIdAct = sessMod.id;
                warehouse.FechaHoraAct = DateTime.Now;
                _context.SaveChanges();

                return new ResponseGeneralModel<bool?>(200, true, MessageHelper.WarehouseDelete);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.WarehouseErrorDelete, ex.ToString());
            }
        }
        public object GetWarehouse()
        {
            return _context.Bodegas
                .Select(b => new
                {
                    b.BodegaId,
                    b.BodegaNombre,
                    b.BodegaDireccion,
                    b.BodegaTelefono,
                    Estado = b.Estado ?? 0,
                    b.FechaHoraReg,
                    b.FechaHoraAct,
                    b.UsuIdReg,
                    b.UsuIdAct,
                    Sucursal = b.SucursalId,
                    SucursalNombre = b.Sucursal != null ? b.Sucursal.SucursalNombre : null
                })
                .ToList();
        }

    }
}
