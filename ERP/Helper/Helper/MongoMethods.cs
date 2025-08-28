using ERP.Helper.Models;
using ERP.Helper.Models.WorkerProcess;
using MongoDB.Bson;
using MongoLibrary.Helper;
using MongoLibrary.Models;
using MongoLibrary.MongoModels;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ERP.Helper.Helper
{
    public class MongoMethods<T>
    {
        public bool SaveLog(LogModel log)
        {
            try
            {
                MongoHelper mongoHelper = (new MethodsHelper<T>()).ConnectionMongo();
                mongoHelper.SaveLog(log);
                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }

        public void SaveProcessMail(SmtpSendRequestModel model)
        {
            SaveProcess("mail", model.ToObject());
        }

        public void SaveProcessPdfWithMail(PdfWithMailWorkerProcessModel model)
        {
            SaveProcess("pdf-mail", model.ToObject());
        }

        public void SaveProcess(string proc, object data)
        {
            MongoHelper mongoHelper = (new MethodsHelper<string>()).ConnectionMongo();
            mongoHelper.SaveProcess(new WorkerProcessModel
            {
                process = proc,
                data = data
            });
        }

        public WorkerProcessMongoModel? GetFirstProcess()
        {
            MongoHelper mongoHelper = (new MethodsHelper<string>()).ConnectionMongo();
            return mongoHelper.GetFirstWorkerProcess();
        }

        public void DeleteProcessWorker(string id)
        {
            MongoHelper mongoHelper = (new MethodsHelper<string>()).ConnectionMongo();
            mongoHelper.DeleteWorkerProcess(id);
        }
    }
}
