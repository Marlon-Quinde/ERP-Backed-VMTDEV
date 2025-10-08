using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Helper;
using ERP.Helper.Models;
using ERP.Models.Security.Module;
using Newtonsoft.Json;

namespace ERP.Bll.Security.Module
{
    public class ModuleBll: IModuleBll
    {
        private readonly BaseErpContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private SessionModel sessMod;
        public ModuleBll(BaseErpContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;

            string token = _httpContext.HttpContext.Request.Headers["token"];
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token requerido");
            }

            MethodsHelper<ModuleRequestModel> metHel = new MethodsHelper<ModuleRequestModel>();
            ResponseGeneralModel<ModuleRequestModel> decToken = metHel.DecodeJwtSession(token);

            if (decToken != null && decToken.code == 200)
            {
                sessMod = JsonConvert.DeserializeObject<SessionModel>(decToken.message);
            }
            else
            {
                throw new UnauthorizedAccessException("Token inválido o expirado");
            }
        }

        public ResponseGeneralModel<string?> CreateModule(ModuleRequestModel request)
        {
            try
            {
                _context.Database.BeginTransaction();
                var ModuleExist = _context.Modulos
                    .FirstOrDefault(p => p.ModuloDescripcion.ToUpper() == request.ModuleDescription.ToUpper());

                if (ModuleExist == null)
                {
                    var LastModule = _context.Modulos.OrderByDescending(p => p.ModuloId).FirstOrDefault();
                    int NewModuleId = (LastModule == null) ? 1 : LastModule.ModuloId + 1;

                    ModuleExist = new Modulo
                    {
                        ModuloId = NewModuleId,
                        ModuloDescripcion = request.ModuleDescription,
                        Estado = 1,
                        UsuIdReg = sessMod.id,
                        FechaHoraAct = DateTime.Now
                    };
                    _context.Modulos.Add(ModuleExist);
                    _context.SaveChanges();
                }
                _context.Database.CommitTransaction();

                return new ResponseGeneralModel<string?>(200, MessageHelper.FormPayCorrect);
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.FormPayIncorrect, ex.ToString());
            }
        }
        public ResponseGeneralModel<bool?> EditModule(int id, ModuleRequestModel requestModel)
        {
            Modulo module = _context.Modulos.First((item) => item.ModuloId == id);

            module.ModuloDescripcion = requestModel.ModuleDescription;
            module.UsuIdAct = sessMod.id;
            module.FechaHoraAct = DateTime.Now;

            _context.SaveChanges();

            return new ResponseGeneralModel<bool?>(200, MessageHelper.ModuleEdit);
        }
        public List<Modulo> GetModule()
        {
            return _context.Modulos.ToList();
        }
    
}
}
