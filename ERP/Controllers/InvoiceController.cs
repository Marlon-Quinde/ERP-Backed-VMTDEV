using ERP.Bll.Invoice;
using ERP.Filters;
using ERP.Helper.Models;
using ERP.Models.Invoice;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP.Controllers
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(SessionUserFilter))]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceBll _bll;
        private readonly ICompositeViewEngine _viewEngine;

        public InvoiceController(IInvoiceBll invoiceBll, ICompositeViewEngine viewEngine)
        {
            _bll = invoiceBll;
            _viewEngine = viewEngine;
        }



        // POST api/<InvoiceController>
        [HttpPost("CreateInvoice")]
        public Task<ResponseGeneralModel<string?>> CreateInvoice([FromBody] InvoiceRequestModel requestM)
        {
            //    HttpContext httpC = HttpContext;

            //    ActionContext actionC = new ActionContext(
            //        httpC,
            //        new RouteData(),
            //    new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor()
            //    );

            //    ViewEngineResult vResult = _viewEngine.FindView(actionC, "Prueba", false);
            return _bll.CreateInvoice(requestM);
        }
    }
}
