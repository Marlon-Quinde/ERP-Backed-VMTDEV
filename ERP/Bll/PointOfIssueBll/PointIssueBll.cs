using ERP.Bll.Location;
using ERP.Bll.PointOfIssueBll;
using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Helper;
using ERP.Helper.Models;
using ERP.Models.PointOfIssue;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERP.Bll.PuntoVenta
{
    public class PointIssueBll : IPointIssueBll
    {
        private readonly BaseErpContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private SessionModel sessMod;

        public PointIssueBll(BaseErpContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;

            string token = _httpContext.HttpContext.Request.Headers["token"];

            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token requerido");
            }

            MethodsHelper<PointIssueRequestModel> metHel = new MethodsHelper<PointIssueRequestModel>();
            ResponseGeneralModel<PointIssueRequestModel> decToken = metHel.DecodeJwtSession(token);

            if (decToken != null && decToken.code == 200)
            {
                sessMod = JsonConvert.DeserializeObject<SessionModel>(decToken.message);
            }
            else
            {
                throw new UnauthorizedAccessException("Token inválido o expirado");
            }
        }

        public ResponseGeneralModel<string?> CrearPuntoEmision(PointIssueRequestModel request)
        {
            try
            {
                _context.Database.BeginTransaction();

                var PuntoEmisionExistente = _context.PuntoEmisionSris
                    .FirstOrDefault(p => p.PuntoEmision.ToUpper() == request.namePointIssue.ToUpper());

                if (PuntoEmisionExistente == null)
                {
                    var ultimoPuntoEmision = _context.PuntoEmisionSris
                        .OrderByDescending(p => p.PuntoEmisionId)
                        .FirstOrDefault();

                    int nuevoPuntoEmisionId = ultimoPuntoEmision == null ? 1 : ultimoPuntoEmision.PuntoEmisionId + 1;

                    PuntoEmisionExistente = new PuntoEmisionSri
                    {
                        PuntoEmisionId = nuevoPuntoEmisionId,
                        PuntoEmision = request.namePointIssue,
                    };

                    _context.PuntoEmisionSris.Add(PuntoEmisionExistente);
                    _context.SaveChanges();
                }

                _context.Database.CommitTransaction();

                return new ResponseGeneralModel<string?>(200, MessageHelper.pointIssueCorrect);
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.pointIssueIncorrect, ex.ToString());
            }
        }

        public List<PuntoEmisionSri> GetPuntoEmisionSri()
        {
            return _context.PuntoEmisionSris.ToList();
        }
    }
}
