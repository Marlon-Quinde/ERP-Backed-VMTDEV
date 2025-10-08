using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Helper;
using ERP.Helper.Models;
using ERP.Models.Products.Brand;
using Newtonsoft.Json;
using System.Data.Entity;


namespace ERP.Bll.Products.Brand
{
    public class BrandBll: IBrandBll
    {
        private readonly BaseErpContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private SessionModel sessMod;
        public BrandBll(BaseErpContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;

            string token = _httpContext.HttpContext.Request.Headers["token"];
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token requerido");
            }

            MethodsHelper<BrandRequestModel> metHel = new MethodsHelper<BrandRequestModel>();
            ResponseGeneralModel<BrandRequestModel> decToken = metHel.DecodeJwtSession(token);

            if (decToken != null && decToken.code == 200)
            {
                sessMod = JsonConvert.DeserializeObject<SessionModel>(decToken.message);
            }
            else
            {
                throw new UnauthorizedAccessException("Token inválido o expirado");
            }
        }

        public ResponseGeneralModel<string?> CreateBrand(BrandRequestModel request)
        {
            try
            {
                _context.Database.BeginTransaction();
                var BrandExist = _context.Marcas
                    .FirstOrDefault(p => p.MarcaDescrip.ToUpper() == request.BrandDescrip.ToUpper());

                if (BrandExist == null)
                {
                    var LastBrandPay = _context.Marcas.OrderByDescending(p => p.MarcaId).FirstOrDefault();
                    int NewBrandId = (LastBrandPay == null) ? 1 : LastBrandPay.MarcaId + 1;

                    BrandExist = new Marca
                    {
                        MarcaId = NewBrandId,
                        MarcaDescrip = request.BrandDescrip,
                        Estado = 1,
                        UsuIdReg = sessMod.id,
                        FechaHoraAct = DateTime.Now
                    };
                    _context.Marcas.Add(BrandExist);
                    _context.SaveChanges();
                }
                _context.Database.CommitTransaction();

                return new ResponseGeneralModel<string?>(200, MessageHelper.BrandCorrect);
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.BrandIncorrect, ex.ToString());
            }
        }
        public ResponseGeneralModel<bool?> EditBrand(int id, BrandRequestModel requestModel)
        {
            Marca brand = _context.Marcas.First((item) => item.MarcaId == id);

            brand.MarcaDescrip = requestModel.BrandDescrip;
            brand.UsuIdAct = sessMod.id;
            brand.FechaHoraAct = DateTime.Now;

            _context.SaveChanges();

            return new ResponseGeneralModel<bool?>(200, MessageHelper.BrandEdit);
        }

        public ResponseGeneralModel<bool?> DeleteBrand(int id)
        {
            try
            {
                Marca? brand = _context.Marcas.FirstOrDefault(r => r.MarcaId == id);
                if (brand == null)
                {
                    return new ResponseGeneralModel<bool?>(404, null, MessageHelper.BrandNotFound);
                }
                brand.Estado = 0;
                brand.UsuIdAct = sessMod.id;
                brand.FechaHoraAct = DateTime.Now;
                _context.SaveChanges();

                return new ResponseGeneralModel<bool?>(200, true, MessageHelper.BrandDelete);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.BrandErrorDelete, ex.ToString());
            }
        }
        public object GetBrand()
        {
            return _context.Marcas
                .Select(m => new
                {
                    m.MarcaId,
                    m.MarcaDescrip,
                    Productos = m.Productos.Select(p => new
                    {
                        p.ProdId,
                        p.ProdDescripcion,
                        p.ProdUltPrecio,
                        p.FechaHoraReg,
                        p.FechaHoraAct,
                        p.UsuIdReg,
                        p.UsuIdAct,
                        p.Estado,
                        p.CategoriaId,
                        p.EmpresaId,
                        p.ProveedorId,
                        p.MarcaId
                    }),
                    Estado = m.Estado == null ? 0 : m.Estado,
                    m.FechaHoraReg,
                    m.FechaHoraAct,
                    m.UsuIdReg,
                    m.UsuIdAct
                })
                .ToList();
        }

    }
}
