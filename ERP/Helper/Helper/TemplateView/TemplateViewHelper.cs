using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ERP.Helper.Helper.TemplateView
{
    public class TemplateViewHelper : ITemplateViewHelper
    {
        private readonly ITempDataProvider _tempDataProvider;
        private readonly ICompositeViewEngine _viewEngine;
        IHttpContextAccessor _httpContext;

        public TemplateViewHelper(ITempDataProvider tempDataProvider, ICompositeViewEngine viewEngine, IHttpContextAccessor httpContextAccessor)
        {
            _tempDataProvider = tempDataProvider;
            _viewEngine = viewEngine;
            _httpContext = httpContextAccessor;
        }

        public async Task<string> RenderViewToStringAsync(string view, object data)
        {
            ActionContext actionC = new ActionContext(
                _httpContext.HttpContext,
                new RouteData(),
                new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor()
            );

            ViewEngineResult vResult = _viewEngine.FindView(actionC, view, false);

            if( vResult.View == null )
            {
                throw new Exception("Vista no existe");
            }

            using (var sw = new StringWriter())
            {
                ViewContext viewC = new ViewContext(
                    actionC,
                    vResult.View,
                    new ViewDataDictionary<object>(
                        new EmptyModelMetadataProvider(),
                        new ModelStateDictionary()
                    )
                    {
                        Model = data
                    },
                    new TempDataDictionary(
                        actionC.HttpContext,
                        _tempDataProvider
                    ),
                    sw,
                    new HtmlHelperOptions()
                );

                await vResult.View.RenderAsync(viewC);

                return sw.ToString();
            }
        }
    }
}
