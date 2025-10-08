using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Helper;
using ERP.Helper.Models;
using ERP.Models.Commercial.Supplier;
using ERP.Models.Company.Company;
using ERP.Models.Inventory.Company;
using Newtonsoft.Json;
using DbEmpresa = ERP.CoreDB.Empresa;


namespace ERP.Bll.Company.Company
{
    public class CompanyBll : ICompanyBll
    {
        private readonly BaseErpContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private SessionModel sessMod;

        public CompanyBll(BaseErpContext context, IHttpContextAccessor httpContext)
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
        public ResponseGeneralModel<string?> CreateCompany(CompanyRequestModel request)
        {
            try
            {
                _context.Database.BeginTransaction();

                var companyExist = _context.Empresas
                    .FirstOrDefault(p => p.EmpresaNombre.ToUpper() == request.CompanyName.ToUpper());

                if (companyExist == null)
                {
                    var lastCompany = _context.Empresas.OrderByDescending(p => p.EmpresaId).FirstOrDefault();
                    int newCompanyId = lastCompany == null ? 1 : lastCompany.EmpresaId + 1;

                    companyExist = new CoreDB.Empresa
                    {
                        EmpresaNombre = request.CompanyName,
                        EmpresaDireccionMatriz = request.CompanyAddressMatrix,
                        EmpresaTelefonoMatriz = request.CompanyPhoneMatrix,
                        EmpresaRazon = request.CompanyReason,
                        EmpresaRuc = request.CompanyRuc,
                        CiudadId = request.CityId,
                        Estado = 1,
                        FechaHoraReg = DateTime.Now,
                        UsuIdReg = sessMod.id
                    };

                    _context.Empresas.Add(companyExist);
                    _context.SaveChanges();
                }

                _context.Database.CommitTransaction();
                return new ResponseGeneralModel<string?>(200, MessageHelper.CompanyCorrect);
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.CompanyIncorrect, ex.ToString());
            }
        }

        public ResponseGeneralModel<bool?> EditCompany(int id,EditCompanyRequestModel requestModel)
        {
            Empresa company = _context.Empresas.First((item) => item.EmpresaId == id);

            company.EmpresaRuc = requestModel.CompanyRuc;
            company.EmpresaDireccionMatriz = requestModel.CompanyAddressMatrix;
            company.EmpresaTelefonoMatriz = requestModel.CompanyPhoneMatrix;
            company.EmpresaNombre = requestModel.CompanyName;
            company.EmpresaRazon = requestModel.CompanyReason;
            company.CiudadId = requestModel.CityId;
            company.UsuIdAct = sessMod.id;
            company.FechaHoraAct = DateTime.Now;
            _context.SaveChanges();

            return new ResponseGeneralModel<bool?>(200, MessageHelper.CompanyEdit);
        }

        //public ResponseGeneralModel<bool?> DeleteCompany(int id)
        //{
        //    try
        //    {
        //        Empresa? company = _context.Empresas.FirstOrDefault(r => r.EmpresaId == id);
        //        if (company == null)
        //        {
        //            return new ResponseGeneralModel<bool?>(404, null, MessageHelper.CompanyNotFound);
        //        }
        //        company.Estado = 0;
        //        company.UsuIdAct = sessMod.id;
        //        company.FechaHoraAct = DateTime.Now;
        //        _context.SaveChanges();

        //        return new ResponseGeneralModel<bool?>(200, true, MessageHelper.CompanyDelete);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseGeneralModel<bool?>(500, null, MessageHelper.CompanyErrorDelete, ex.ToString());
        //    }
        //}
        public object GetCompany()
        {
            return _context.Empresas
                .Select(c => new
                {
                    c.EmpresaId,
                    c.EmpresaRuc,
                    c.EmpresaNombre,
                    c.EmpresaRazon,
                    c.EmpresaDireccionMatriz,
                    c.EmpresaTelefonoMatriz,
                    c.CiudadId,
                    Estado = c.Estado == null ? 0 : c.Estado, 
                    c.FechaHoraReg,
                    c.FechaHoraAct,
                    c.UsuIdReg,
                    c.UsuIdAct
                })
                .ToList();
        }

    }
}
