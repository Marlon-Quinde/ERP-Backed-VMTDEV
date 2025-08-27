using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Helper;
using ERP.Helper.Models;
using ERP.Models.Client;
using ERP.Models.Security.Authentication;
using Newtonsoft.Json;

namespace ERP.Bll.Empresa
{
    public class ClientBll : IClientBll
    {
        private readonly BaseErpContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private SessionModel sessMod;

        public ClientBll(BaseErpContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;

            string token = _httpContext.HttpContext.Request.Headers["token"];
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token requerido");
            }

            MethodsHelper<ClientRequestModel> metHel = new MethodsHelper<ClientRequestModel>();
            ResponseGeneralModel<ClientRequestModel> decToken = metHel.DecodeJwtSession(token);

            if (decToken != null && decToken.code == 200)
            {
                sessMod = JsonConvert.DeserializeObject<SessionModel>(decToken.message);
            }
            else
            {
                throw new UnauthorizedAccessException("Token inválido o expirado");
            }
        }

        public ResponseGeneralModel<string?> CreateClient(ClientRequestModel request)
        {
            try
            {
                _context.Database.BeginTransaction();
                var clientExist = _context.Clientes
                    .FirstOrDefault(p => p.ClienteNombre1.ToUpper() == request.name1.ToUpper());

                if (clientExist == null)
                {
                    var ultimClient = _context.Clientes.OrderByDescending(p => p.ClienteId).FirstOrDefault();
                    int nuevoClientId = (ultimClient == null) ? 1 : ultimClient.ClienteId + 1;

                    clientExist = new Cliente
                    {
                        ClienteId = nuevoClientId,
                        ClienteNombre1 = request.name1,
                        ClienteNombre2 = request.name2,
                        ClienteApellido1 = request.lastname1,
                        ClienteApellido2 = request.lastname2,
                        ClienteDireccion = request.address,
                        ClienteEmail = request.email,
                        ClienteRuc = request.ruc,
                        ClienteTelefono = request.phone,
                        Estado = 1,
                    };
                    _context.Clientes.Add(clientExist);
                    _context.SaveChanges();
                }

                _context.Database.CommitTransaction();

                return new ResponseGeneralModel<string?>(200, MessageHelper.clientCorrect);
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.clienteError, ex.ToString());
            }
        }

        public List<Cliente> GetCliente()
        {
            return _context.Clientes.ToList();
        }
    }
}
