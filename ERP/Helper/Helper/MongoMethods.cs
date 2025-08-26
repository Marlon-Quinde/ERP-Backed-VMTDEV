using ERP.Helper.Models.WorkerProcess;
using MongoLibrary.Helper;
using MongoLibrary.Models;
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

        public void SaveProcessMail(WorkerProcessMailModel model)
        {
            /*SaveProcess("mail", JsonConvert.DeserializeObject<object>(JsonConvert.SerializeObject(model)) ?? new
            {

            });*/
            SaveProcess("mail", new
            {
                to = model.to,
                subject = model.subject,
                body = model.body,
            });
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
    }
}
