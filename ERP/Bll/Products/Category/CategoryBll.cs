using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Helper;
using ERP.Helper.Models;
using ERP.Models.Products.Brand;
using ERP.Models.Products.Category;
using Newtonsoft.Json;
using System.Data.Entity;

namespace ERP.Bll.Products.Category
{
    public class CategoryBll : ICategoryBll 
    {
        private readonly BaseErpContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private SessionModel sessMod;
        public CategoryBll(BaseErpContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;

            string token = _httpContext.HttpContext.Request.Headers["token"];
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token requerido");
            }

            MethodsHelper<CategoryRequestModel> metHel = new MethodsHelper<CategoryRequestModel>();
            ResponseGeneralModel<CategoryRequestModel> decToken = metHel.DecodeJwtSession(token);

            if (decToken != null && decToken.code == 200)
            {
                sessMod = JsonConvert.DeserializeObject<SessionModel>(decToken.message);
            }
            else
            {
                throw new UnauthorizedAccessException("Token inválido o expirado");
            }
        }

        public ResponseGeneralModel<string?> CreateCategory(CategoryRequestModel request)
        {
            try
            {
                _context.Database.BeginTransaction();
                var CategoryExist = _context.Categoria
                    .FirstOrDefault(p => p.CategoriaDescrip.ToUpper() == request.CategoryDescrip.ToUpper());

                if (CategoryExist == null)
                {
                    var LastCategory = _context.Categoria.OrderByDescending(p => p.CategoriaId).FirstOrDefault();
                    int NewCategoryId = (LastCategory  == null) ? 1 : LastCategory.CategoriaId + 1;

                    CategoryExist = new Categorium
                    {
                        CategoriaId = NewCategoryId,
                        CategoriaDescrip = request.CategoryDescrip,
                        Estado = 1,
                        UsuIdReg = sessMod.id,
                        FechaHoraAct = DateTime.Now
                    };
                    _context.Categoria.Add(CategoryExist);
                    _context.SaveChanges();
                }
                _context.Database.CommitTransaction();

                return new ResponseGeneralModel<string?>(200, MessageHelper.CategoryCorrect);
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.CategoryIncorrect, ex.ToString());
            }
        }
        public ResponseGeneralModel<bool?> EditCategory(int id, CategoryRequestModel requestModel)
        {
            Categorium category = _context.Categoria.First((item) => item.CategoriaId == id);

            category.CategoriaDescrip = requestModel.CategoryDescrip;
            category.UsuIdAct = sessMod.id;
            category.FechaHoraAct = DateTime.Now;

            _context.SaveChanges();

            return new ResponseGeneralModel<bool?>(200, MessageHelper.CategoryEdit);
        }

        public ResponseGeneralModel<bool?> DeleteCategory(int id)
        {
            try
            {
                Categorium? category = _context.Categoria.FirstOrDefault(r => r.CategoriaId == id);
                if (category == null)
                {
                    return new ResponseGeneralModel<bool?>(404, null, MessageHelper.CategoryNotFound);
                }
                category.Estado = 0;
                category.UsuIdAct = sessMod.id;
                category.FechaHoraAct = DateTime.Now;
                _context.SaveChanges();

                return new ResponseGeneralModel<bool?>(200, true, MessageHelper.CategoryDelete);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.CategoryErrorDelete, ex.ToString());
            }
        }
        public object GetCategory()
        {
            return _context.Categoria
                 .Select(m => new
                 {
                     m.CategoriaId,
                     m.CategoriaDescrip,
                     Productos = m.Productos.Select(p => new
                     {
                         p.ProdId,
                         p.ProdDescripcion,
                         
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
