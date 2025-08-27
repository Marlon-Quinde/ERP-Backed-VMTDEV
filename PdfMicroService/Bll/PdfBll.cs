using HelperGeneral.Models;
using PdfMicroService.Helper.Helper.Pdf;
using PdfMicroService.Models;
using System.Text;

namespace PdfMicroService.Bll
{
    public class PdfBll : IPdfBll
    {
        IPdfHelper _pdfHelper;

        public PdfBll(IPdfHelper pdfHelper)
        {
            _pdfHelper = pdfHelper;
        }

        public ResponseData<string?> CreatePdf(CreatePdfRequest request)
        {
            byte[] fileByte = _pdfHelper.CreatePdf(request.html);
            string base64 = Convert.ToBase64String(fileByte);
            return new ResponseData<string?>(base64);
        }

        public byte[] CreatePdf2(CreatePdfRequest request)
        {
            return _pdfHelper.CreatePdf(request.html);
        }
    }
}
