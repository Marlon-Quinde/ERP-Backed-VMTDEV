using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Helper;
using ERP.Helper.Models;
using ERP.Models.Company.Branch;
using Newtonsoft.Json;

namespace ERP.Bll.Company.Branch
{
    public class BranchBll : IBranchBll
    {
        private readonly BaseErpContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private SessionModel sessMod;

        public BranchBll(BaseErpContext context, IHttpContextAccessor httpContext)
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
        public ResponseGeneralModel<string?> CreateBranch(BranchRequestModel request)
        {
            try
            {
                _context.Database.BeginTransaction();

                var branchExist = _context.Sucursals
                    .FirstOrDefault(p => p.SucursalNombre.ToUpper() == request.BranchName.ToUpper()
                                         && p.EmpresaId == request.CompanyId);

                if (branchExist == null)
                {
                    var lastBranch = _context.Sucursals.OrderByDescending(p => p.SucursalId).FirstOrDefault();
                    int newBranchId = lastBranch == null ? 1 : lastBranch.SucursalId + 1;

                    branchExist = new Sucursal
                    {
                        SucursalId = newBranchId,
                        EmpresaId = request.CompanyId,
                        SucursalNombre = request.BranchName,
                        SucursalDireccion = request.BranchAddress,
                        SucursalTelefono = request.BranchPhone,
                        Bodegas = new List<Bodega>(),
                        CodEstablecimientoSri = request.CodEstablSri,
                        Estado = 1,
                        FechaHoraReg = DateTime.Now,
                        UsuIdReg = sessMod.id
                    };

                    _context.Sucursals.Add(branchExist);
                    _context.SaveChanges();
                }

                _context.Database.CommitTransaction();
                return new ResponseGeneralModel<string?>(200, MessageHelper.BranchCorrect);
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.BranchIncorrect, ex.ToString());
            }
        }
        public ResponseGeneralModel<bool?> EditBranch(int id, EditBranchRequestModel requestModel)
        {
            Sucursal branch = _context.Sucursals.First((item) => item.SucursalId == id);

            branch.SucursalRuc = requestModel.BranchRuc;
            branch.SucursalDireccion = requestModel.BranchAddress;
            branch.SucursalTelefono = requestModel.BranchPhone;
            branch.SucursalNombre = requestModel.BranchName;
            branch.UsuIdAct = sessMod.id;
            branch.FechaHoraAct = DateTime.Now;
            _context.SaveChanges();

            return new ResponseGeneralModel<bool?>(200, MessageHelper.BranchEdit);
        }

        //public ResponseGeneralModel<bool?> DeleteBranch(int id)
        //{
        //    try
        //    {
        //        Sucursal? branch = _context.Sucursals.FirstOrDefault(r => r.SucursalId == id);
        //        if (branch == null)
        //        {
        //            return new ResponseGeneralModel<bool?>(404, null, MessageHelper.BranchNotFound);
        //        }
        //        branch.Estado = 0;
        //        branch.UsuIdAct = sessMod.id;
        //        branch.FechaHoraAct = DateTime.Now;
        //        _context.SaveChanges();

        //        return new ResponseGeneralModel<bool?>(200, true, MessageHelper.BranchDelete);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseGeneralModel<bool?>(500, null, MessageHelper.BranchErrorDelete, ex.ToString());
        //    }
        //}
        public object GetBranches()
        {
            return _context.Sucursals
                .Select(b => new
                {
                    b.SucursalId,
                    b.EmpresaId,
                    b.SucursalNombre,
                    b.SucursalDireccion,
                    b.SucursalTelefono,
                    b.CodEstablecimientoSri,
                    Estado = b.Estado == null ? 0 : b.Estado, 
                    b.FechaHoraReg,
                    b.FechaHoraAct,
                    b.UsuIdReg,
                    b.UsuIdAct
                })
                .ToList();
        }

    }
}
