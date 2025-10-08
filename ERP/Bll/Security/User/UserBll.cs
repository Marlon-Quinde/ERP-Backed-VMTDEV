using ERP.CoreDB;
using ERP.Helper.Helper;
using ERP.Helper.Models;
using Newtonsoft.Json;

namespace ERP.Bll.Security.User
{
    public class UserBll : IUserBll
    {
        private readonly BaseErpContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private SessionModel sessMod;

        public UserBll(BaseErpContext context, IHttpContextAccessor httpContext)
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

        public List<Usuario> GetUsers()
        {
            return _context.Usuarios.ToList();
        }
    }
}
