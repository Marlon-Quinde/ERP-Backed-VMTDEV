using Newtonsoft.Json;

namespace ERP.Helper.Models
{
    public class PdfWithMailWorkerProcessModel
    {
        public SmtpSendRequestModel mail { get; set; }
        public PdfWithMailWorkerProcessModel_Pdf pdf { get; set; }


        public object ToObject()
        {
            return new
            {
                mail = mail.ToObject(),
                pdf = pdf.ToObject()
            };
        }

        public PdfWithMailWorkerProcessModel FromObject(object data)
        {
            //return new PdfWithMailWorkerProcessModel
            //{
            //    mail = mail.FromObject(data),
            //    pdf = pdf.FromObject(data)
            //};
            return JsonConvert.DeserializeObject<PdfWithMailWorkerProcessModel>(JsonConvert.SerializeObject(data));
        }
    }

    public class PdfWithMailWorkerProcessModel_Pdf
    {
        public string html { get; set; }

        public object ToObject()
        {
            return new
            {
                html = html
            };
        }

        public PdfWithMailWorkerProcessModel_Pdf FromObject(object data)
        {
            return JsonConvert.DeserializeObject<PdfWithMailWorkerProcessModel_Pdf>(JsonConvert.SerializeObject(data));
        }
    }
}
