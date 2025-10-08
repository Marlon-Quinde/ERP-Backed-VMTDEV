using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Helper;
using ERP.Helper.Models;
using ERP.Models.Pay.CreditCard;
using ERP.Models.Pay.FormPay;
using Newtonsoft.Json;

namespace ERP.Bll.Pay
{
    public class FormPayBll : IFormPayBll
    {
        private readonly BaseErpContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private SessionModel sessMod;
        public FormPayBll(BaseErpContext context, IHttpContextAccessor httpContext)
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

        public ResponseGeneralModel<string?> CreateFormPago(FormPayRequestModel request)
        {
            try
            {
                _context.Database.BeginTransaction();
                var FormPayExist = _context.FormaPagos
                    .FirstOrDefault(p => p.FpagoDescripcion.ToUpper() == request.FormPayDescription.ToUpper());

                if (FormPayExist == null)
                {
                    var LastFormPay = _context.FormaPagos.OrderByDescending(p => p.FpagoId).FirstOrDefault();
                    int NewFormPayId = (LastFormPay == null) ? 1 : LastFormPay.FpagoId + 1;

                    FormPayExist = new FormaPago
                    {
                        FpagoId = NewFormPayId,
                        FpagoDescripcion = request.FormPayDescription,
                        Estado = 1,
                        UsuIdReg= sessMod.id,
                        FechaHoraAct = DateTime.Now
                    };
                    _context.FormaPagos.Add(FormPayExist);
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
        public ResponseGeneralModel<bool?> EditFormPago(int id, FormPayRequestModel requestModel)
        {
            FormaPago formPay = _context.FormaPagos.First((item) => item.FpagoId == id);

            formPay.FpagoDescripcion = requestModel.FormPayDescription;
            formPay.UsuIdAct = sessMod.id;
            formPay.FechaHoraAct = DateTime.Now;

            _context.SaveChanges();

            return new ResponseGeneralModel<bool?>(200, MessageHelper.FormPayEdit);
        }
        public object GetFormPago()
        {
            return _context.FormaPagos
                .Select(f => new
                {
                    f.FpagoId,
                    f.FpagoDescripcion,
                    f.FechaHoraReg,
                    f.FechaHoraAct,
                    f.UsuIdReg,
                    f.UsuIdAct
                })
                .ToList();
        }

      


        public ResponseGeneralModel<string?> CreateCreditCard(CreditCardRequestModel request)
        {
            try
            {
                _context.Database.BeginTransaction();
                var CreditCardExist = _context.TarjetaCreditos
                    .FirstOrDefault(p => p.TarjetacredDescripcion.ToUpper() == request.CreditCardDescription.ToUpper());

                if (CreditCardExist == null)
                {
                    var LastCreditCard = _context.TarjetaCreditos.OrderByDescending(p => p.TarjetacredId).FirstOrDefault();
                    int NewCreditCardId = (LastCreditCard == null) ? 1 : LastCreditCard.TarjetacredId + 1;

                    CreditCardExist = new TarjetaCredito
                    {
                        TarjetacredDescripcion = request.CreditCardDescription,
                        IndustriaId = request.IndustryId,
                        UsuIdReg = sessMod.id,
                        FechaHoraAct = DateTime.Now
                    };
                    _context.TarjetaCreditos.Add(CreditCardExist);
                    _context.SaveChanges();
                }
                _context.Database.CommitTransaction();

                return new ResponseGeneralModel<string?>(200, MessageHelper.CreditCardCorrect);
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.CreditCardIncorrect, ex.ToString());
            }
        }
        public ResponseGeneralModel<bool?> EditCreditCard(int id, CreditCardRequestModel requestModel)
        {
            TarjetaCredito creditCard = _context.TarjetaCreditos.First((item) => item.TarjetacredId == id);

            creditCard.TarjetacredDescripcion = requestModel.CreditCardDescription;
            creditCard.UsuIdAct = sessMod.id;
            creditCard.FechaHoraAct = DateTime.Now;

            _context.SaveChanges();

            return new ResponseGeneralModel<bool?>(200, MessageHelper.CreditCardEdit);
        }
        public object GetCreditCard()
        {
            return _context.TarjetaCreditos
                .Select(c => new
                {
                    c.TarjetacredId,
                    c.TarjetacredDescripcion,
                    c.IndustriaId,
                    c.FechaHoraReg,
                    c.FechaHoraAct,
                    c.UsuIdReg,
                    c.UsuIdAct
                })
                .ToList();
        }
    }
}
