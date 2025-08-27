using ERP.Bll.PointOfIssueBll;
using ERP.CoreDB;
using ERP.Filters;
using ERP.Helper.Data;
using ERP.Helper.Models;
using ERP.Models.PointOfIssue;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ERP.Controllers.PointOfIssue
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointIssueController : ControllerBase
    {
        private readonly IPointIssueBll pointIssueBll;

        public PointIssueController(IPointIssueBll bll)
        {
            pointIssueBll = bll;
        }

        [HttpGet("listar punto de emision")]
        public ResponseGeneralModel<List<PuntoEmisionSri>?> GetPuntoEmisionSri()
        {
            try
            {
                return new ResponseGeneralModel<List<PuntoEmisionSri>?>(200, pointIssueBll.GetPuntoEmisionSri(), MessageHelper.pointIssueListCorrect);
            }
            catch (Exception ex)
            {
                return new ResponseGeneralModel<List<PuntoEmisionSri>?>(500, null, MessageHelper.errorGeneral, ex.ToString());
            }
        }


        [HttpPost("Crear")]
        public ResponseGeneralModel<string?> CrearPuntoEmision([FromBody] PointIssueRequestModel request)
        {
            return pointIssueBll.CrearPuntoEmision(request);
        }
    }
}
