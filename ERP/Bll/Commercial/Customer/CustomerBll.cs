using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Helper;
using ERP.Helper.Models;
using ERP.Models.Commercial.Customer;
using ERP.Models.Security.Role;
using Newtonsoft.Json;
using System.Data.Entity;

namespace ERP.Bll.Commercial.Customer
{
    public class CustomerBll : ICustomerBll
    {
        private readonly BaseErpContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private SessionModel sessMod;

        public CustomerBll(BaseErpContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;

            string token = _httpContext.HttpContext.Request.Headers["token"];
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token requerido");
            }

            MethodsHelper<CustomerRequestModel> metHel = new MethodsHelper<CustomerRequestModel>();
            ResponseGeneralModel<CustomerRequestModel> decToken = metHel.DecodeJwtSession(token);

            if (decToken != null && decToken.code == 200)
            {
                sessMod = JsonConvert.DeserializeObject<SessionModel>(decToken.message);
            }
            else
            {
                throw new UnauthorizedAccessException("Token inválido o expirado");
            }
        }

        public ResponseGeneralModel<string?> CreateCustomer(CustomerRequestModel request)
        {
            try
            {
                _context.Database.BeginTransaction();
                var CustomerExist = _context.Clientes
                    .FirstOrDefault(p => p.ClienteNombre1.ToUpper() == request.CustomerName1.ToUpper());

                if (CustomerExist == null)
                {
                    var LastCustomer = _context.Clientes.OrderByDescending(p => p.ClienteId).FirstOrDefault();
                    int NewCustomerId = LastCustomer == null ? 1 : LastCustomer.ClienteId + 1;

                    CustomerExist = new Cliente
                    {
                        ClienteId = NewCustomerId,
                        ClienteNombre1 = request.CustomerName1,
                        ClienteNombre2 = request.CustomerName2,
                        ClienteApellido1 = request.CustomerLastName1,
                        ClienteApellido2 = request.CustomerLastName2,
                        ClienteDireccion = request.CustomerAddress,
                        ClienteEmail = request.CustomerEmail,
                        ClienteRuc = request.CustomerRuc,
                        ClienteTelefono = request.CustomerPhone,
                        Estado = 1,
                        UsuIdReg = sessMod.id,
                        FechaHoraReg = DateTime.Now
                    };
                    _context.Clientes.Add(CustomerExist);
                    _context.SaveChanges();
                }
                _context.SaveChanges();
                _context.Database.CommitTransaction();

                return new ResponseGeneralModel<string?>(200, MessageHelper.CustomerCorrect);
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.CustomerError, ex.ToString());
            }
        }
        public ResponseGeneralModel<bool?> EditCustomer(int id, EditCustomerRequestModel requestModel)
        {
            Cliente customer = _context.Clientes.First((item) => item.ClienteId == id);

            customer.ClienteRuc = requestModel.CustomerRuc;
            customer.ClienteEmail = requestModel.CustomerEmail;
            customer.ClienteApellido1 = requestModel.CustomerLastName1;
            customer.ClienteApellido2 = requestModel.CustomerLastName2;
            customer.ClienteDireccion = requestModel.CustomerAddress;
            customer.ClienteTelefono = requestModel.CustomerPhone;
            customer.UsuIdAct = sessMod.id;
            customer.FechaHoraAct = DateOnly.FromDateTime(DateTime.Now);
            _context.SaveChanges();

            return new ResponseGeneralModel<bool?>(200, MessageHelper.CustomerEdit);
        }

        public ResponseGeneralModel<bool?> DeleteCustomer(int id)
        {
            try
            {
                _context.Database.BeginTransaction();

                Cliente? customer = _context.Clientes.FirstOrDefault(r => r.ClienteId == id);
                if (customer == null)
                {
                    return new ResponseGeneralModel<bool?>(404, null, MessageHelper.CustomerNotFound);
                }
                customer.Estado = 0;
                customer.UsuIdAct = sessMod.id;
                customer.FechaHoraAct = customer.FechaHoraAct = DateOnly.FromDateTime(DateTime.Now);
                _context.SaveChanges();
                _context.Database.CommitTransaction();
                return new ResponseGeneralModel<bool?>(200, true, MessageHelper.CustomerDelete);
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new ResponseGeneralModel<bool?>(500, null, MessageHelper.CustomerErrorDelete, ex.ToString());
            }
        }
        public object GetCustomer()
        {
            return _context.Clientes
                .Where(c => c.Estado == 1)
                .Include(c => c.MovimientoCabs) // carga las cabeceras de movimiento asociadas
                .Select(c => new
                {
                    c.ClienteId,
                    c.ClienteRuc,
                    c.ClienteNombre1,
                    c.ClienteNombre2,
                    c.ClienteApellido1,
                    c.ClienteApellido2,
                    c.ClienteEmail,
                    c.ClienteTelefono,
                    c.ClienteDireccion,
                    c.Estado,
                    c.FechaHoraReg,
                    c.FechaHoraAct,
                    c.UsuIdReg,
                    c.UsuIdAct,
                    MovimientoCabs = c.MovimientoCabs.Select(m => new
                    {
                        m.MovicabId,
                        m.SecuenciaFactura,
                        m.FechaHoraReg,
                        m.ClienteId,
                        m.ProveedorId
                    }).ToList()
                })
                .ToList();
        }

    }
}
