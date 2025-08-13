using ERP.Helper.Data;
using ERP.Helper.Helper;
using ERP.Helper.Models;
using ERP.Models.Security.Profile;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ERP.Filters
{
    public class SessionUserFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                string token = context.HttpContext.Request.Headers["token"];
                MethodsHelper<ProfileResponseModel> metHel = new MethodsHelper<ProfileResponseModel>();
                ResponseGeneralModel<ProfileResponseModel> decToken = metHel.DecodeJwtSession(token);
                if (decToken.code != 200)
                {
                    context.HttpContext.Response.WriteAsJsonAsync(
                        decToken
                    );
                }
                else
                {
                    await next();
                }
            } catch (Exception ex)
            {
                context.HttpContext.Response.WriteAsJsonAsync(
                    new ResponseGeneralModel<string?>(MessageHelper.errorGeneral, ex.ToString())
                );
            }
        }
    }
}
