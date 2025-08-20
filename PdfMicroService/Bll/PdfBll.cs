using PdfMicroService.Helper.Helper.Pdf;
using PdfMicroService.Models;

namespace PdfMicroService.Bll
{
    public class PdfBll : IPdfBll
    {
        IPdfHelper _pdfHelper;

        public PdfBll(IPdfHelper pdfHelper)
        {
            _pdfHelper = pdfHelper;
        }

        public byte[] CreatePdf(CreatePdfRequest request)
        {
            return _pdfHelper.CreatePdf(request.html);
        }
    }
}
