using ERP.Bll.Invoice;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP.Controllers
{
    [Route("api/[controller]")]
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
        public Task<string> CreateInvoice([FromBody] string value)
        {
            //    HttpContext httpC = HttpContext;

            //    ActionContext actionC = new ActionContext(
            //        httpC,
            //        new RouteData(),
            //    new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor()
            //    );

            //    ViewEngineResult vResult = _viewEngine.FindView(actionC, "Prueba", false);
            return _bll.CreateInvoice(value);
        }
    }
}
