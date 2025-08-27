using ERP.Helper.Helper;
using System.Diagnostics;

namespace ERP.Helper.Models
{
    public class ResponseGeneralModel<T>
    {
        public int code { get; set; }
        public T data { get; set; }
        public string message { get; set; }
        public string messageDev { get; set; }

        public ResponseGeneralModel(int code, string message)
        {
            this.code = code;
            this.message = message;
        }

        public ResponseGeneralModel(int code, T data, string message, string messageDev)
        {
            this.code = code;
            this.message = message;
            bool isDebug = (new ConfigurationBuilder()).AddJsonFile("appsettings.json").Build().GetSection("isDebug").Get<bool>();
            if (isDebug)
            {
                this.messageDev = messageDev;
            }
            SaveLog(messageDev);
        }

        public ResponseGeneralModel(string message, string messageDev)
        {
            this.code = 400;
            this.message = message;
            bool isDebug = (new ConfigurationBuilder()).AddJsonFile("appsettings.json").Build().GetSection("isDebug").Get<bool>();
            if (isDebug)
            {
                this.messageDev = messageDev;
            }
            SaveLog(messageDev);
        }

        public ResponseGeneralModel(int code, T data, string message)
        {
            this.code = code;
            this.data = data;
            this.message = message;
        }

        public ResponseGeneralModel(string message, string messageDev, int code)
        {
            this.code = code;
            this.message = message;
            bool isDebug = (new ConfigurationBuilder()).AddJsonFile("appsettings.json").Build().GetSection("isDebug").Get<bool>();
            if (isDebug)
            {
                this.messageDev = messageDev;
            }
            SaveLog(messageDev);
        }


        private void SaveLog(string ex)
        {
            MongoMethods<T> mongoM = new MongoMethods<T>();
            mongoM.SaveLog(new MongoLibrary.Models.LogModel()
            {
                isError = true,
                data = new
                {
                    message = ex,
                }
            });
        }
    }
}
