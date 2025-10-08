using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Helper;
using ERP.Helper.Models;
using ERP.Models.Products.Industry;
using Newtonsoft.Json;

namespace ERP.Bll.Products.Industry
{
    public class IndustryBll : IIndustryBll
    {
        private readonly BaseErpContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private SessionModel sessMod;
        public IndustryBll(BaseErpContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;

            string token = _httpContext.HttpContext.Request.Headers["token"];
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token requerido");
            }

            MethodsHelper<IndustryRequestModel> metHel = new MethodsHelper<IndustryRequestModel>();
            ResponseGeneralModel<IndustryRequestModel> decToken = metHel.DecodeJwtSession(token);

            if (decToken != null && decToken.code == 200)
            {
                sessMod = JsonConvert.DeserializeObject<SessionModel>(decToken.message);
            }
            else
            {
                throw new UnauthorizedAccessException("Token inválido o expirado");
            }
        }

        public ResponseGeneralModel<string?> CreateIndustry(IndustryRequestModel request)
        {
            try
            {
                _context.Database.BeginTransaction();
                var IndustryExist = _context.Industria
                    .FirstOrDefault(p => p.IndustriaDescripcion.ToUpper() == request.IndustryDescription.ToUpper());

                if (IndustryExist == null)
                {
                    var LastIndustry = _context.Industria.OrderByDescending(p => p.IndustriaId).FirstOrDefault();
                    int NewIndustryId = (LastIndustry == null) ? 1 : LastIndustry.IndustriaId + 1;

                    IndustryExist = new Industrium
                    {
                        IndustriaId = NewIndustryId,
                        IndustriaDescripcion = request.IndustryDescription,
                        Estado = 1,
                        UsuIdReg = sessMod.id,
                        FechaHoraAct = DateTime.Now
                    };
                    _context.Industria.Add(IndustryExist);
                    _context.SaveChanges();
                }
                _context.Database.CommitTransaction();

                return new ResponseGeneralModel<string?>(200, MessageHelper.IndustryCorrect);
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.IndustryIncorrect, ex.ToString());
            }
        }
        public ResponseGeneralModel<bool?> EditIndustry(int id, IndustryRequestModel requestModel)
        {
            Industrium industry = _context.Industria.First((item) => item.IndustriaId == id);

            industry.IndustriaDescripcion = requestModel.IndustryDescription;
            industry.UsuIdAct = sessMod.id;
            industry.FechaHoraAct = DateTime.Now;

            _context.SaveChanges();

            return new ResponseGeneralModel<bool?>(200, MessageHelper.IndustryEdit);
        }
        public object GetIndustry()
        {
            return _context.Industria.Select(m => new
                {
                m.IndustriaId,
                    m.IndustriaDescripcion,
                    TarjetaCredito = m.TarjetaCreditos.Select(p => new
                    {
                        p.TarjetacredId
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
