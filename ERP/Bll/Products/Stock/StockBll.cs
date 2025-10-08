using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Helper;
using ERP.Helper.Models;
using ERP.Models.Products.Stock;
using Newtonsoft.Json;
using System.Data.Entity;

namespace ERP.Bll.Products.Stock
{
    public class StockBll: IStockBll
    {
        private readonly BaseErpContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private SessionModel sessMod;
        public StockBll(BaseErpContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;

            string token = _httpContext.HttpContext.Request.Headers["token"];
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token requerido");
            }

            MethodsHelper<StockRequestModel> metHel = new MethodsHelper<StockRequestModel>();
            ResponseGeneralModel<StockRequestModel> decToken = metHel.DecodeJwtSession(token);

            if (decToken != null && decToken.code == 200)
            {
                sessMod = JsonConvert.DeserializeObject<SessionModel>(decToken.message);
            }
            else
            {
                throw new UnauthorizedAccessException("Token inválido o expirado");
            }
        }

        public ResponseGeneralModel<string?> CreateStock(StockRequestModel request)
        {
            try
            {
                _context.Database.BeginTransaction();

                var stockExist = _context.Stocks
                    .FirstOrDefault(s =>
                        s.EmpresaId == request.CompanyId &&
                        s.SucursalId == request.BranchId &&
                        s.BodegaId == request.WarehouseId &&
                        s.ProdId == request.ProdId
                    );

                if (stockExist == null)
                {
                    var lastStock = _context.Stocks
                        .OrderByDescending(s => s.StockId)
                        .FirstOrDefault();

                    long newStockId = lastStock == null ? 1 : lastStock.StockId + 1;

                    var stock = new CoreDB.Stock
                    {
                        StockId = newStockId,
                        EmpresaId = request.CompanyId ?? 0,
                        SucursalId = request.BranchId ?? 0,
                        BodegaId = request.WarehouseId ?? 0,
                        ProdId = request.ProdId ?? 0,
                        CantidadStock = request.AmountStock ?? 0,
                        Estado = 1,
                        UsuIdReg = sessMod.id,
                        FechaHoraReg = DateTime.Now
                    };

                    _context.Stocks.Add(stock);
                    _context.SaveChanges();
                }
                else
                {
                    return new ResponseGeneralModel<string?>(400, null, MessageHelper.StockIncorrect);
                }

                _context.Database.CommitTransaction();
                return new ResponseGeneralModel<string?>(200, MessageHelper.StockCorrect);
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.StockIncorrect, ex.ToString());
            }
        }

   
        public ResponseGeneralModel<bool?> EditStock(int id, StockRequestModel request)
        {
            var stock = _context.Stocks.FirstOrDefault(s => s.StockId == id);
            if (stock == null)
                return new ResponseGeneralModel<bool?>(404, null, MessageHelper.StockNotFound);

            stock.EmpresaId = request.CompanyId ?? stock.EmpresaId;
            stock.SucursalId = request.BranchId ?? stock.SucursalId;
            stock.BodegaId = request.WarehouseId ?? stock.BodegaId;
            stock.ProdId = request.ProdId ?? stock.ProdId;
            stock.CantidadStock = request.AmountStock ?? stock.CantidadStock;
            stock.UsuIdAct = sessMod.id;
            stock.FechaHoraAct = DateTime.Now;

            _context.SaveChanges();

            return new ResponseGeneralModel<bool?>(200, MessageHelper.StockEdit);
        }
        public object GetAllStock()
        {
            return _context.Stocks
                .Select(s => new
                {
                    s.StockId,
                    s.EmpresaId,
                    s.SucursalId,
                    s.BodegaId,
                    s.ProdId,
                    s.CantidadStock,
                    Estado = s.Estado ?? 0,
                    s.FechaHoraReg,
                    s.FechaHoraAct,
                    s.UsuIdReg,
                    s.UsuIdAct
                })
                .ToList();
        }


    }
}
