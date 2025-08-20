using PdfMicroService.Models;

namespace PdfMicroService.Bll
{
    public interface IPdfBll
    {
        public byte[] CreatePdf(CreatePdfRequest request);
    }
}
