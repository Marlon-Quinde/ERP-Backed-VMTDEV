namespace PdfMicroService.Helper.Helper.Pdf
{
    public interface IPdfHelper
    {
        public byte[] CreatePdf(string html);
    }
}
