using HelperGeneral.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PdfMicroService.Bll;
using PdfMicroService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PdfMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        IPdfBll _pdfBll;

        public PdfController(IPdfBll pdfBll)
        {
            _pdfBll = pdfBll;
        }

        // POST api/<PdfController>
        [HttpPost("CreatePdf")]
        public ResponseData<string?> CreatePDf([FromBody] CreatePdfRequest request)
        {
            return _pdfBll.CreatePdf(request);
        }

        [HttpPost("ShowPdf")]
        public async Task<IActionResult> ShowPdf([FromBody] CreatePdfRequest request)
        {
            byte[] fileByte = _pdfBll.CreatePdf2(request);
            return File(fileByte, "application/pdf", "archivo.pdf");
        }
    }
}
