using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Helper;
using ERP.Helper.Models;
using ERP.Models.Pay.FormPay;
using ERP.Models.Security.Module;
using ERP.Models.Security.Options;
using Newtonsoft.Json;

namespace ERP.Bll.Security.Option
{
    public class OptionBll : IOptionBll
    {
        private readonly BaseErpContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private SessionModel sessMod;
        public OptionBll(BaseErpContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;

            string token = _httpContext.HttpContext.Request.Headers["token"];
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token requerido");
            }

            MethodsHelper<OptionRequestModel> metHel = new MethodsHelper<OptionRequestModel>();
            ResponseGeneralModel<OptionRequestModel> decToken = metHel.DecodeJwtSession(token);

            if (decToken != null && decToken.code == 200)
            {
                sessMod = JsonConvert.DeserializeObject<SessionModel>(decToken.message);
            }
            else
            {
                throw new UnauthorizedAccessException("Token inválido o expirado");
            }
        }

        public ResponseGeneralModel<string?> CreateOption(OptionRequestModel request)
        {
            try
            {
                _context.Database.BeginTransaction();
                var OptionExist = _context.Opcions
                    .FirstOrDefault(p => p.OpcionDescripcion.ToUpper() == request.OptionDescription.ToUpper());

                if (OptionExist == null)
                {
                    var LastOpcion = _context.Opcions.OrderByDescending(p => p.OpcionId).FirstOrDefault();
                    int NewOpcionId = (LastOpcion == null) ? 1 : LastOpcion.OpcionId + 1;

                    OptionExist = new Opcion
                    {
                        OpcionId = NewOpcionId,
                        OpcionDescripcion = request.OptionDescription,
                        ModuloId = request.ModuleId,    
                        Estado = 1,
                        UsuIdReg = sessMod.id,
                        FechaHoraAct = DateTime.Now
                    };
                    _context.Opcions.Add(OptionExist);
                    _context.SaveChanges();
                }
                _context.Database.CommitTransaction();

                return new ResponseGeneralModel<string?>(200, MessageHelper.OptionCorrect);
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.OptionIncorrect, ex.ToString());
            }
        }
        public ResponseGeneralModel<bool?> EditOption(int id, OptionRequestModel requestModel)
        {
            Opcion option = _context.Opcions.First((item) => item.OpcionId == id);

            option.OpcionDescripcion = requestModel.OptionDescription;
            option.ModuloId = requestModel.ModuleId;
            option.UsuIdAct = sessMod.id;
            option.FechaHoraAct = DateTime.Now;

            _context.SaveChanges();

            return new ResponseGeneralModel<bool?>(200, MessageHelper.OptionEdit);
        }
        public List<Opcion> GetOption()
        {
            return _context.Opcions.ToList();
        }

    }
}
