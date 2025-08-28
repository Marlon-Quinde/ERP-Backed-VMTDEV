
using ERP.Helper.Helper;
using ERP.Helper.Models;
using ERP.Helper.Models.WorkerProcess;
using MongoLibrary.MongoModels;
using Newtonsoft.Json;

namespace ERP.WorkerService
{
    public class ProcessWorkerService : BackgroundService
    {
        IConfiguration _configuration;
        static bool isRunning = false;
        private readonly TimeSpan _period = TimeSpan.FromSeconds(3);

        public ProcessWorkerService(IConfiguration configuration) {
            _configuration = configuration;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                using var timer = new PeriodicTimer(_period);
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    try
                    {
                        if (!isRunning)
                        {
                            isRunning = true;

                            WorkerProcessMongoModel? exist = (new MongoMethods<string>()).GetFirstProcess();

                            if (exist != null)
                            {
                                switch(exist.process)
                                {
                                    case "mail":
                                        SmtpSendRequestModel smtpObjEmail = JsonConvert.DeserializeObject<SmtpSendRequestModel>(JsonConvert.SerializeObject(exist.data));
                                        ExternalServiceHelper extSerH = new ExternalServiceHelper();
                                        //SmtpSendRequestModel smtpObjEmail = new SmtpSendRequestModel()
                                        //{
                                        //    To = modelMail.to,
                                        //    Subject = modelMail.subject,
                                        //    Body = modelMail.body,
                                        //    Files = modelMail.file,
                                        //};
                                        var dataS = await extSerH.PostServiceExternal(_configuration.GetSection("services").GetValue<string>("smtp"), jsonData: smtpObjEmail.ToJson());
                                        if(dataS.code == 200)
                                        {
                                            (new MongoMethods<string>()).DeleteProcessWorker(exist.id);
                                        }
                                        break;
                                    case "pdf-mail":
                                        PdfWithMailWorkerProcessModel pdfObj = new PdfWithMailWorkerProcessModel().FromObject(exist.data);
                                        ExternalServiceHelper extSerHPdf = new ExternalServiceHelper();
                                        ResponseGeneralModel<string?> respPdf = await extSerHPdf.PostServiceExternal(_configuration.GetSection("services").GetValue<string>("pdf"), jsonData: JsonConvert.SerializeObject(pdfObj.pdf));
                                        if (respPdf.code == 200)
                                        {
                                            ResponseExternalServiceModel<string?> respPdfServ = ResponseExternalServiceModel<string?>.FromJson(respPdf.data);
                                            if (respPdfServ.IsTrue)
                                            {
                                                SmtpSendRequestModel mailReq = pdfObj.mail;
                                                mailReq.Files[0].Base64 = respPdfServ.Data;
                                                (new MongoMethods<string>()).SaveProcessMail(mailReq);
                                                (new MongoMethods<string>()).DeleteProcessWorker(exist.id);
                                            }
                                        }
                                        break;
                                }
                            }

                            isRunning = false;
                        }
                    } catch (Exception ex)
                    {
                        isRunning = false;
                    }
                }
            } catch (Exception ex)
            {
                //
            }
        }
    }
}
