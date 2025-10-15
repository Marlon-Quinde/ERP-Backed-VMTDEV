using ERP.Bll.Products.Category;
using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Models;
using ERP.Models.Products.Category;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP.Controllers.Products.Category
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryBll categoryBll;

        public CategoryController(ICategoryBll bll)
        {
            categoryBll = bll;
        }

        [HttpGet("Listar-Categorìa")]
        public ResponseGeneralModel<object?> GetCategory()
        {
            try
            {
                return new ResponseGeneralModel<object?>(200, categoryBll.GetCategory(), MessageHelper.CategoryCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<object?>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }

        [HttpPost("Crear-Categorìa")]
        public ResponseGeneralModel<string?> CreateCategory([FromBody] CategoryRequestModel request)
        {
            return categoryBll.CreateCategory(request);
        }

        [HttpPut("Actualizar-Categorìa")]
        public ResponseGeneralModel<bool?> Put(int id, [FromBody] CategoryRequestModel requestModel)
        {
            try
            {
                return categoryBll.EditCategory(id, requestModel);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }

        [HttpDelete("{id}")]
        public ResponseGeneralModel<bool?> DeteleCategory(int id)
        {
            try
            {
                return categoryBll.DeleteCategory(id);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }

        [HttpGet("{id}")]
        public ResponseGeneralModel<object?> FindCategory(string id)
        {
            try
            {
                return new ResponseGeneralModel<object?>(200, categoryBll.FindCategory(id), MessageHelper.CategoryCorrect);
            }
            catch (Exception e)
            {
                return new ResponseGeneralModel<object?>(500, null, MessageHelper.errorGeneral, e.ToString());
            }
        }
    }
}
