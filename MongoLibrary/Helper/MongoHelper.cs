using MongoDB.Driver;
using MongoLibrary.Models;
using MongoLibrary.MongoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoLibrary.Helper
{
    public class MongoHelper
    {

        MongoClient _client;
        IMongoDatabase _db;

        IMongoCollection<LogMongoModel> _collectLog;
        IMongoCollection<WorkerProcessMongoModel> _collectWorkerProcess;

        public MongoHelper(string stringConnect, string nameDataBase)
        {
            _client = new MongoClient(stringConnect);
            _db = _client.GetDatabase(nameDataBase);

            _collectLog = _db.GetCollection<LogMongoModel>("Log");
            _collectWorkerProcess = _db.GetCollection<WorkerProcessMongoModel>("WorkerProcess");
        }


        public void SaveLog(LogModel logM)
        {
            LogMongoModel logModel = new LogMongoModel()
            {
                isError = logM.isError,
                data = logM.data,
                dateT = DateTime.Now
            };

            _collectLog.InsertOne(logModel);
        }

        public void SaveProcess(WorkerProcessModel processModel)
        {
            WorkerProcessMongoModel processMongoModel = new WorkerProcessMongoModel()
            {
                data = processModel.data,
                process = processModel.process,
                dateT = DateTime.Now
            };

            _collectWorkerProcess.InsertOne(processMongoModel);
        }

        public WorkerProcessMongoModel GetFirstWorkerPRocess()
        {
            var filter = Builders<WorkerProcessMongoModel>.Filter.Where(x => x.dateT < DateTime.Now.AddSeconds(-5));
            return _collectWorkerProcess.Find(filter).First();
        }
    }
}
