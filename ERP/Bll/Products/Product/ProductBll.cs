using Azure.Core;
using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Helper;
using ERP.Helper.Models;
using ERP.Models.Products.Product;
using Newtonsoft.Json;
using System.Data.Entity;

namespace ERP.Bll.Products.Product
{
    public class ProductBll : IProductBll
    {
        private readonly BaseErpContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private SessionModel sessMod;
        public ProductBll(BaseErpContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;

            string token = _httpContext.HttpContext.Request.Headers["token"];
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token requerido");
            }

            MethodsHelper<ProductRequestModel> metHel = new MethodsHelper<ProductRequestModel>();
            ResponseGeneralModel<ProductRequestModel> decToken = metHel.DecodeJwtSession(token);

            if (decToken != null && decToken.code == 200)
            {
                sessMod = JsonConvert.DeserializeObject<SessionModel>(decToken.message);
            }
            else
            {
                throw new UnauthorizedAccessException("Token inválido o expirado");
            }
        }

        public ResponseGeneralModel<string?> CreateProduct(ProductRequestModel request)
        {
            try
            {
                _context.Database.BeginTransaction();
                var ProductExist = _context.Productos
                    .FirstOrDefault(p => p.ProdDescripcion.ToUpper() == request.ProdDescription.ToUpper());

                if (ProductExist == null)
                {
                    var LastProduct = _context.Productos.OrderByDescending(p => p.ProdId).FirstOrDefault();
                    int NewProductId = (LastProduct == null) ? 1 : LastProduct.ProdId + 1;

                    ProductExist = new Producto     
                    {
                        ProdId = NewProductId,
                        ProdDescripcion = request.ProdDescription,
                        ProdUltPrecio = request.ProdUltPrice,
                        CategoriaId = request.CategoryId,
                        EmpresaId = request.CompanyId,
                        ProveedorId = request.SupplierId,
                        MarcaId = request.BrandId,
                        UsuIdAct = sessMod.id,
                        FechaHoraAct = DateTime.Now,
                        UsuIdReg = sessMod.id,  
                        FechaHoraReg = DateTime.Now,
                        Estado = 1
                        
                    };
                    {
                        
                    };
                    _context.Productos.Add(ProductExist);
                    _context.SaveChanges();
                }
                _context.Database.CommitTransaction();

                return new ResponseGeneralModel<string?>(200, MessageHelper.ProductCorrect);
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.ProductIncorrect, ex.ToString());
            }
        }
        public ResponseGeneralModel<bool?> EditProduct(int id, ProductRequestModel requestModel)
        {
            Producto product = _context.Productos.First((item) => item.ProdId == id);

            product.ProdDescripcion = requestModel.ProdDescription;
            product.ProdUltPrecio = requestModel.ProdUltPrice;
            product.CategoriaId = requestModel.CategoryId;
            product.EmpresaId = requestModel.CompanyId;
            product.ProveedorId = requestModel.SupplierId;
            product.MarcaId = requestModel.BrandId;
            product.UsuIdAct = sessMod.id;
            product.FechaHoraAct = DateTime.Now;

            _context.SaveChanges();

            return new ResponseGeneralModel<bool?>(200, MessageHelper.ProductEdit);
        }

        public ResponseGeneralModel<bool?> DeleteProduct(int id)
        {
            try
            {
                Producto? product = _context.Productos.FirstOrDefault(r => r.ProdId == id);
                if (product == null)
                {
                    return new ResponseGeneralModel<bool?>(404, null, MessageHelper.ProductNotFound);
                }
                product.Estado = 0;
                product.UsuIdAct = sessMod.id;
                product.FechaHoraAct = DateTime.Now;
                _context.SaveChanges();

                return new ResponseGeneralModel<bool?>(200, true, MessageHelper.ProductDelete);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.ProductErrorDelete, ex.ToString());
            }
        }
        public object GetProduct()
        {
            return _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Marca)
                .Include(p => p.MovimientoDetProductos) 
                .Where(p => p.Estado == 1)
                .Select(p => new
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
                    p.MarcaId,
                    Categoria = p.Categoria == null ? null : new
                    {
                        p.Categoria.CategoriaId,
                        p.Categoria.CategoriaDescrip
                    },
                    Marca = p.Marca == null ? null : new
                    {
                        p.Marca.MarcaId,
                        p.Marca.MarcaDescrip
                    },
                    MovimientoDetProductos = p.MovimientoDetProductos.Select(mdp => new
                    {
                        mdp.MovidetProdId,
                        mdp.Cantidad,
                        mdp.Precio,
                        MovimientoCab = mdp.Movicab == null ? null : new
                        {
                            mdp.Movicab.MovicabId,
                            mdp.Movicab.SecuenciaFactura
                        }
                    })
                })
                .ToList();
        }


    }
}
