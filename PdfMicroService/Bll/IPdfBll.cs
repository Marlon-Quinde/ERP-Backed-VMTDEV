using HelperGeneral.Models;
using PdfMicroService.Models;

namespace PdfMicroService.Bll
{
    public interface IPdfBll
    {
        public ResponseData<string?> CreatePdf(CreatePdfRequest request);
        public byte[] CreatePdf2(CreatePdfRequest request);
    }
}
