using ERP.CoreDB;
using ERP.Helper.Helper;
using ERP.Helper.Helper.TemplateView;
using ERP.Helper.Models;
using ERP.Helper.Models.Views.Pdf;
using ERP.Helper.Models.WorkerProcess;
using ERP.Models.Invoice;
using ERP.Models.Security.Profile;
using Newtonsoft.Json;

namespace ERP.Bll.Invoice
{
    public class InvoiceBll : IInvoiceBll
    {

        private readonly ITemplateViewHelper _templateViewHelper;
        IConfiguration _configuration;

        BaseErpContext _context;
        IHttpContextAccessor _httpContext;

        SessionModel sessMod;

        public InvoiceBll(BaseErpContext context, IHttpContextAccessor httpContext, ITemplateViewHelper templateViewHelper, IConfiguration configuration)
        {
            _context = context;
            _httpContext = httpContext;
            _templateViewHelper = templateViewHelper;

            string token = _httpContext.HttpContext.Request.Headers["token"];
            if (token == "") return;
            MethodsHelper<ProfileResponseModel> metHel = new MethodsHelper<ProfileResponseModel>();
            ResponseGeneralModel<ProfileResponseModel> decToken = metHel.DecodeJwtSession(token);


            if (decToken != null)
            {
                if (decToken.code == 200)
                {
                    sessMod = JsonConvert.DeserializeObject<SessionModel>(decToken.message);
                }
            }
            _configuration = configuration;
        }

        public async Task<ResponseGeneralModel<string?>> CreateInvoice(InvoiceRequestModel value)
        {
            List<int> listIdProd = value.products.Select(y => y.productId).ToList();
            List<Producto> listProdDB = _context.Productos.Where(x => listIdProd.Contains(x.ProdId)).ToList();

            if(listProdDB.Count != listIdProd.Count)
            {
                return new ResponseGeneralModel<string?>(500, "Uno de los productos no existe");
            }


            List<ElectronicInvoiceViewPdf_products> prodctPdf = new List<ElectronicInvoiceViewPdf_products>();
            decimal total = 0;
            foreach( InvoiceRequestModel_products products in value.products)
            {
                Producto prodFind = listProdDB.First(x => x.ProdId == products.productId);
                decimal totProd = products.qty * (prodFind.ProdUltPrecio ?? 0);
                total += totProd;
                prodctPdf.Add(new ElectronicInvoiceViewPdf_products(
                    products.qty,
                    prodFind.ProdDescripcion,
                    prodFind.ProdUltPrecio ?? 0,
                    totProd
                ));
            }



            string renderPdf = await _templateViewHelper.RenderViewToStringAsync("Pdf/ElectronicInvoice", new ElectronicInvoiceViewPdf()
            {
                namePerson = sessMod.name,
                total = total,
                products = prodctPdf
            });

            (new MongoMethods<WorkerProcessMailModel>()).SaveProcessPdfWithMail(new PdfWithMailWorkerProcessModel
            {
                mail = new SmtpSendRequestModel()
                {
                    To = "jmoran@viamatica.com",
                    Subject = "Project Invoice SMTP",
                    Body = "Factura creada",
                    Files = new List<SmtpSendRequestModel_File>()
                    {
                        new SmtpSendRequestModel_File()
                        {
                            Name = "Factura.pdf",
                            TypeFile = "application/pdf",
                            Base64 = ""
                        }
                    }
                },
                pdf = new PdfWithMailWorkerProcessModel_Pdf
                {
                    html = renderPdf
                },
            });

            //ExternalServiceHelper extSerH = new ExternalServiceHelper();
            //ResponseGeneralModel<string?> respPdf = await extSerH.PostServiceExternal(_configuration.GetSection("services").GetValue<string>("pdf"), jsonData: JsonConvert.SerializeObject(new
            //{
            //    html = renderPdf,
            //}));
            //if (respPdf.code != 200) return respPdf;

            //ResponseExternalServiceModel<string?> respPdfServ = ResponseExternalServiceModel<string?>.FromJson(respPdf.data);
            //if (!respPdfServ.IsTrue) return new ResponseGeneralModel<string?>(500, respPdfServ.Message);


            // Process Mongo
            /*(new MongoMethods<WorkerProcessMailModel>()).SaveProcessMail(new Helper.Models.WorkerProcess.WorkerProcessMailModel()
            {
                to = "jmoran@viamatica.com",
                subject = "Project Invoice SMTP",
                body = "Factura creada",
                file = new List<SmtpSendRequestModel_File>()
                {
                    new SmtpSendRequestModel_File()
                    {
                        Name = "Factura.pdf",
                        TypeFile = "application/pdf",
                        Base64 = respPdfServ.Data
                    }
                }
            });
            //*/
            //(new MongoMethods<WorkerProcessMailModel>()).SaveProcessMail(new SmtpSendRequestModel()
            //{
            //    To = "jmoran@viamatica.com",
            //    Subject = "Project Invoice SMTP",
            //    Body = "Factura creada",
            //    Files = new List<SmtpSendRequestModel_File>()
            //    {
            //        new SmtpSendRequestModel_File()
            //        {
            //            Name = "Factura.pdf",
            //            TypeFile = "application/pdf",
            //            Base64 = respPdfServ.Data
            //        }
            //    }
            //});
            // .......


            //SmtpSendRequestModel smtpObjEmail = new SmtpSendRequestModel()
            //{
            //    To = "jmoran@viamatica.com",
            //    Subject = "Project Invoice SMTP",
            //    Body = "Factura creada",
            //    Files = new List<SmtpSendRequestModel_File>()
            //    {
            //        new SmtpSendRequestModel_File()
            //        {
            //            Name = "Factura.pdf",
            //            TypeFile = "application/pdf",
            //            Base64 = respPdfServ.Data
            //        }
            //    }
            //};
            //extSerH.PostServiceExternal(_configuration.GetSection("services").GetValue<string>("smtp"), jsonData: smtpObjEmail.ToJson());

            return new ResponseGeneralModel<string?>(200, renderPdf, "");
        }
    }
}
