using ERP.Bll.PointSaleBll;
using ERP.CoreDB;
using ERP.Helper.Data;
using ERP.Helper.Helper;
using ERP.Helper.Models;
using ERP.Models.PointOfIssue;
using ERP.Models.PointOfSale;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERP.Bll.PointSaleBll
{
    public class PointSaleBll : IPointSaleBll
    {
        private readonly BaseErpContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private SessionModel sessMod;
        public PointSaleBll(BaseErpContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;

            string token = _httpContext.HttpContext.Request.Headers["token"];

            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token requerido");
            }

            MethodsHelper<PointSaleRequestModel> metHel = new MethodsHelper<PointSaleRequestModel>();
            ResponseGeneralModel<PointSaleRequestModel> decToken = metHel.DecodeJwtSession(token);

            if (decToken != null && decToken.code == 200)
            {
                sessMod = JsonConvert.DeserializeObject<SessionModel>(decToken.message);
            }
            else
            {
                throw new UnauthorizedAccessException("Token inválido o expirado");
            }
        }

        public ResponseGeneralModel<string?> CreatePointSale(PointSaleRequestModel request)
        {
            try
            {
                _context.Database.BeginTransaction();

                var existingPointSale = _context.PuntoVenta
                    .FirstOrDefault(p => p.PuntovtaNombre!.ToUpper() == request.PointSaleName.ToUpper());

                if (existingPointSale == null)
                {
                    var lastPointSale = _context.PuntoVenta
                        .OrderByDescending(p => p.PuntovtaId)
                        .FirstOrDefault();

                    int newPointSaleId = lastPointSale == null ? 1 : lastPointSale.PuntovtaId + 1;

                    var newPointSale = new PuntoVentum
                    {
                        PuntovtaId = newPointSaleId,
                        PuntovtaNombre = request.PointSaleName,
                        Estado = 1,
                        FechaHoraReg = DateTime.Now,
                        FechaHoraAct = DateTime.Now,

                        PuntoEmisionId = request.PointEmissionId,
                        SucursalId = request.SucursalId,
                        UsuIdReg = request.UserId,
                        UsuIdAct = request.UserId
                    };

                    var puntoEmision = _context.PuntoEmisionSris
                        .FirstOrDefault(p => p.PuntoEmisionId == request.PointEmissionId);
                    if (puntoEmision == null)
                    {
                        return new ResponseGeneralModel<string?>(400, null, MessageHelper.pointSaleIncorrect);
                    }

                    _context.PuntoVenta.Add(newPointSale);

                    _context.SaveChanges();
                }

                _context.Database.CommitTransaction();
                return new ResponseGeneralModel<string?>(200, MessageHelper.pointSaleCorrect);

            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new ResponseGeneralModel<string?>(500, null, MessageHelper.pointSaleListIncorrect, ex.ToString());
            }
        }

        public ResponseGeneralModel<List<PointSaleResponseModel>> GetPointSales()
        {
            try
            {
                var list = _context.PuntoVenta
                    .Select(p => new PointSaleResponseModel
                    {
                        PointSaleId = p.PuntovtaId,
                        PointSaleName = p.PuntovtaNombre!,
                        Status = p.Estado ?? 0,
                        RegistrationDate = p.FechaHoraReg,
                        UpdateDate = p.FechaHoraAct
                    })
                    .ToList();

                return new ResponseGeneralModel<List<PointSaleResponseModel>>(200, list, MessageHelper.pointSaleListCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<List<PointSaleResponseModel>>(500, null, MessageHelper.pointSaleListIncorrect, ex.ToString());
            }
        }
    }
}