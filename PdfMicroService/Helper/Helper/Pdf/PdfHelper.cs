using DinkToPdf;
using DinkToPdf.Contracts;

namespace PdfMicroService.Helper.Helper.Pdf
{
    public class PdfHelper : IPdfHelper
    {
        private readonly IConverter _convert;

        GlobalSettings _globalSettings;
        HtmlToPdfDocument _document;

        public PdfHelper(IConverter convert)
        {
            _convert = convert;

            _globalSettings = new GlobalSettings() {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Out = null
            };
        }

        private void InitConfig(string html)
        {
            _document = new HtmlToPdfDocument()
            {
                GlobalSettings = _globalSettings,
                Objects =
                {
                    new ObjectSettings()
                    {
                        HtmlContent = html,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontName = "Arial", FontSize = 9, Left = "Mi empresa S.A.", Line = true },
                        FooterSettings = { FontName = "Arial", FontSize = 9, Right = "Página [page]/[toPage]", Line = true }
                    }
                }
            };
        }

        public byte[] CreatePdf(string html)
        {
            InitConfig(html);

            return _convert.Convert(_document);
        }
    }
}
