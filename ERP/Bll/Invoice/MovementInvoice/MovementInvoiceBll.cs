using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Helper;
using ERP.Helper.Models;
using ERP.Models.Invoice.MovemetCab;
using ERP.Models.Invoice.MovemetDetPay;
using ERP.Models.Invoice.MovemetDetProduct;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;


namespace ERP.Bll.Invoice.MovementInvoice
{
    public class MovementInvoiceBll : IMovementInvoiceBll
    {
        private readonly BaseErpContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private SessionModel sessMod;

        public MovementInvoiceBll(BaseErpContext context, IHttpContextAccessor httpContext)
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

       //MOVIMIENTOCAB
        public ResponseGeneralModel<string?> CreateMovemetCab(MovemetCabRequestModel request)
        {
            try
            {
                var LastMovementCab = _context.MovimientoCabs.OrderByDescending(m => m.MovicabId).FirstOrDefault();
                int NewMovementCabId = LastMovementCab == null ? 1 : LastMovementCab.MovicabId + 1;

                var MovementCab = new MovimientoCab
                {
                    MovicabId = NewMovementCabId,
                    TipomovId = request.TypeMovId,
                    TipomovIngEgr = request.TypeMovIngEgr,
                    EmpresaId = request.CompanyId,
                    SucursalId = request.BranchId,
                    BodegaId = request.WarehouseId,
                    SecuenciaFactura = request.SecuenceInvoice,
                    AutorizacionSri = request.AutorizationSri,
                    ClaveAcceso = request.AccessKey,
                    ClienteId = request.CustomerId,
                    PuntovtaId = request.PointSaleId,
                    ProveedorId = request.SupplierId,
                    Estado = "1",
                    FechaHoraReg = DateTime.Now,
                    UsuIdReg = sessMod.id
                };

                _context.MovimientoCabs.Add(MovementCab);
                _context.SaveChanges();

                return new ResponseGeneralModel<string?>(200, MessageHelper.MovementCabCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.MovementCabIncorrect, ex.ToString());
            }
        }

        public ResponseGeneralModel<bool?> EditMovementCab(int id, MovemetCabRequestModel request)
        {
            var MovementCab = _context.MovimientoCabs.FirstOrDefault(m => m.MovicabId == id);
            if (MovementCab == null)
                return new ResponseGeneralModel<bool?>(404, null, MessageHelper.MovementCabNotFound);

            MovementCab.TipomovId = request.TypeMovId;
            MovementCab.TipomovIngEgr = request.TypeMovIngEgr;
            MovementCab.EmpresaId = request.CompanyId;
            MovementCab.SucursalId = request.BranchId;
            MovementCab.BodegaId = request.WarehouseId;
            MovementCab.SecuenciaFactura = request.SecuenceInvoice;
            MovementCab.AutorizacionSri = request.AutorizationSri;
            MovementCab.ClaveAcceso = request.AccessKey;
            MovementCab.ClienteId = request.CustomerId;
            MovementCab.PuntovtaId = request.PointSaleId;
            MovementCab.ProveedorId = request.SupplierId;
            MovementCab.UsuIdAct = sessMod.id;
            MovementCab.FechaHoraAct = DateTime.Now;

            _context.SaveChanges();

            return new ResponseGeneralModel<bool?>(200, MessageHelper.MovementCabEdit);
        }

        public object GetMovementCab()
        {
            return _context.MovimientoCabs
                .Include(mc => mc.Cliente)                
                .Include(mc => mc.Proveedor)            
                .Include(mc => mc.MovimientoDetProductos) 
                .Include(mc => mc.MovimientoDetPagos)     
                .Select(mc => new
                {
                    mc.MovicabId,
                    mc.TipomovId,
                    mc.TipomovIngEgr,
                    mc.EmpresaId,
                    mc.SucursalId,
                    mc.BodegaId,
                    mc.SecuenciaFactura,
                    mc.AutorizacionSri,
                    mc.ClaveAcceso,
                    ClienteNombre = mc.Cliente != null ? mc.Cliente.ClienteNombre1 : null,
                    ProveedorNombre = mc.Proveedor != null ? mc.Proveedor.ProvNomComercial : null,
                    mc.Estado,
                    mc.FechaHoraReg,
                    mc.FechaHoraAct,
                    mc.UsuIdReg,
                    mc.UsuIdAct,
                    Productos = mc.MovimientoDetProductos.Select(p => new
                    {
                        p.ProductoId,
                        p.Cantidad,
                        p.Precio
                    }),
                    Pagos = mc.MovimientoDetPagos.Select(p => new
                    {
                        p.MovidetPagoId,
                       
                    })
                })
                .ToList();
        }


        //MOVIMIENTOPAGOS
        public ResponseGeneralModel<string?> CreateMovementDetPay(MovemetDetPayRequestModel request)
        {
            try
            {
                var LastMovementPay = _context.MovimientoDetPagos.OrderByDescending(d => d.MovidetPagoId).FirstOrDefault();
                int NewMovementPayId = LastMovementPay == null ? 1 : LastMovementPay.MovidetPagoId + 1;

                var MovementDetPay = new MovimientoDetPago
                {
                    MovidetPagoId = NewMovementPayId,
                    MovicabId = request.MovicabId,
                    FpagoId = request.FormPayId,
                    ValorPagado = request.ValuePaid,
                    IndustriaId = request.IndustryId,
                    Voucher = request.Voucher,
                    Lote=request.Batch,
                    TarjetacredId = request.CreditCardId,
                    BancoId = request.BankId,
                    ComprobanteId = request.ComprbnId,
                    FechaPago = request.DatePay,
                    ClienteId = request.CustomerId,
                };

                _context.MovimientoDetPagos.Add(MovementDetPay);
                _context.SaveChanges();

                return new ResponseGeneralModel<string?>(200, MessageHelper.MovementDetProductCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<string?>(500, null,MessageHelper.MovementPayIncorrect, ex.ToString());
            }
        }

        public ResponseGeneralModel<bool?> EditMovementDetPay(int id, MovemetDetPayRequestModel request)
        {
            var detPay = _context.MovimientoDetPagos.FirstOrDefault(d => d.MovidetPagoId == id);
            if (detPay == null)
                return new ResponseGeneralModel<bool?>(404, null, MessageHelper.MovementPayNotFound);

            detPay.MovicabId = request.MovicabId;
            detPay.FpagoId = request.FormPayId;
            detPay.ValorPagado = request.ValuePaid;
            detPay.IndustriaId = request.IndustryId;
            detPay.Lote = request.Batch;
            detPay.Voucher = request.Voucher;
            detPay.TarjetacredId = request.CreditCardId;
            detPay.BancoId = request.BankId;
            detPay.ComprobanteId = request.ComprbnId;
            detPay.FechaPago = request.DatePay;
            detPay.ClienteId = request.CustomerId;
            _context.SaveChanges();

            return new ResponseGeneralModel<bool?>(200, MessageHelper.MovementDetProductEdit);
        }

        public object GetMovementDetPay()
        {
            return _context.MovimientoDetPagos
                .Include(p => p.Fpago)
                .Include(p => p.Movicab)
                .Include(p => p.Tarjetacred)
                .Select(p => new
                {
                    p.MovidetPagoId,
                    p.MovicabId,
                    p.FpagoId,
                    p.ValorPagado,
                    p.IndustriaId,
                    p.Lote,
                    p.Voucher,
                    p.TarjetacredId,
                    p.BancoId,
                    p.ComprobanteId,
                    p.FechaPago,
                    p.ClienteId,

                    FormaPago = p.FechaPago != null ? p.Fpago.FpagoDescripcion : null,
                    MovimientoCabecera = p.Movicab != null ? p.Movicab.SecuenciaFactura : null,
                    TarjetaCredito = p.TarjetacredId != null ? p.Tarjetacred.TarjetacredDescripcion : null
                })
                .ToList();
        }


        // MOVIMIENTOPRODUCTO
        public ResponseGeneralModel<string?> CreateMovemetDetProduct(MovemetDetProductRequestModel request)
        {
            try
            {
                var LastMovementDetProducts = _context.MovimientoDetProductos.OrderByDescending(d => d.MovidetProdId).FirstOrDefault();
                int NewMovementDetProductosId = LastMovementDetProducts == null ? 1 : LastMovementDetProducts.MovidetProdId + 1;

                var MovementDetProducts = new MovimientoDetProducto
                {
                    MovidetProdId = NewMovementDetProductosId,
                    MovicabId = request.MovcabId,
                    ProductoId = request.ProductId,
                    Cantidad = request.Amount,
                    Precio = request.Price,
                    Estado = 1,
                    FechaHoraReg = DateTime.Now,
                    UsuIdReg = sessMod.id
                };

                _context.MovimientoDetProductos.Add(MovementDetProducts);
                _context.SaveChanges();

                return new ResponseGeneralModel<string?>(200, MessageHelper.MovementDetProductCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.MovementDetProductIncorrect, ex.ToString());
            }
        }

        public ResponseGeneralModel<bool?> EditMovementDetProduct(int id, MovemetDetProductRequestModel request)
        {
            var MovementProduct = _context.MovimientoDetProductos.FirstOrDefault(d => d.MovidetProdId == id);
            if (MovementProduct == null)
                return new ResponseGeneralModel<bool?>(404, null, MessageHelper.MovementDetProductNotFound);

            MovementProduct.MovidetProdId = request.MovdetProdId;
            MovementProduct.ProductoId = request.ProductId;
            MovementProduct.Cantidad = request.Amount;
            MovementProduct.Precio = request.Price;
            MovementProduct.UsuIdAct = sessMod.id;
            MovementProduct.FechaHoraAct = DateTime.Now;

            _context.SaveChanges();

            return new ResponseGeneralModel<bool?>(200, MessageHelper.MovementDetProductEdit);
        }

        public object GetMovementDetProduct()
        {
            return _context.MovimientoDetProductos
                .Include(m => m.Producto)
                .Include(m => m.Movicab)
                .Where(m => m.Producto != null && m.Movicab != null && m.Cantidad != null && m.Precio != null)
                .Select(m => new
                {
                    m.MovidetProdId,
                    m.Cantidad,
                    m.Precio,
                    Producto = new
                    {
                        m.Producto.ProdId,
                        m.Producto.ProdDescripcion
                    },
                    MovimientoCab = new
                    {
                        m.Movicab.MovicabId,
                        m.Movicab.SecuenciaFactura
                    }
                })
                .ToList();
        }



    }
}