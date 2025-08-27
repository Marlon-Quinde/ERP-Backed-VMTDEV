using ERP.Bll.Location;
using ERP.Helper.Models;
using ERP.Models.test;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Controllers.Location
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        private readonly IPaisBll _paisBll;

        public PaisController(IPaisBll paisBll)
        {
            _paisBll = paisBll;
        }

        [HttpPost("Crear")]
        public ResponseGeneralModel<string?> CrearPaisYCiudad([FromBody] TestDbCommitRequestModel request)
        {
            return _paisBll.CrearPaisYCiudad(request);
        }
    }
}
