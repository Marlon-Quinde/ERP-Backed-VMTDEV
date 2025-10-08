using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Helper;
using ERP.Helper.Models;
using ERP.Models.Products.Tax;
using Newtonsoft.Json;

namespace ERP.Bll.Products.Tax
{
    public class TaxBll : ITaxBll   
    {
        private readonly BaseErpContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private SessionModel sessMod;
        public TaxBll(BaseErpContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;

            string token = _httpContext.HttpContext.Request.Headers["token"];
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token requerido");
            }

            MethodsHelper<TaxRequestModel> metHel = new MethodsHelper<TaxRequestModel>();
            ResponseGeneralModel<TaxRequestModel> decToken = metHel.DecodeJwtSession(token);

            if (decToken != null && decToken.code == 200)
            {
                sessMod = JsonConvert.DeserializeObject<SessionModel>(decToken.message);
            }
            else
            {
                throw new UnauthorizedAccessException("Token inválido o expirado");
            }
        }

        public ResponseGeneralModel<string?> CreateTax(TaxRequestModel request)
        {
            try
            {
                _context.Database.BeginTransaction();

                var taxExist = _context.Impuestos
                    .FirstOrDefault(p => p.ImpuestoDescr.ToUpper() == request.TaxDescr!.ToUpper());

                if (taxExist == null)
                {
                    var tax = new Impuesto
                    {
                        ImpuestoDescr = request.TaxDescr,
                        ImpuestoValor = request.TaxValue ?? 0,
                    };

                    _context.Impuestos.Add(tax);
                    _context.SaveChanges();
                }
                else
                {
                    return new ResponseGeneralModel<string?>(400, null, MessageHelper.TaxIncorrect);
                }

                _context.Database.CommitTransaction();
                return new ResponseGeneralModel<string?>(200, MessageHelper.TaxCorrect);
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.TaxIncorrect, ex.ToString());
            }
        }
        public ResponseGeneralModel<bool?> EditTax(int id, TaxRequestModel requestModel)
        {
            Impuesto tax = _context.Impuestos.First((item) => item.ImpuestoId == id);

            tax.ImpuestoDescr = requestModel.TaxDescr;
            tax.ImpuestoValor = requestModel.TaxValue;

            _context.SaveChanges();

            return new ResponseGeneralModel<bool?>(200, MessageHelper.TaxEdit);
        }

        public object GetTax()
        {
            return _context.Impuestos
                .Select(t => new
                {
                    t.ImpuestoId,
                    t.ImpuestoDescr,
                    t.ImpuestoValor,
                   
                })
                .ToList();
        }
    }
}
