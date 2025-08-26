using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoLibrary.Models
{
    public class WorkerProcessModel
    {
        public string process { get; set; }
        public object data { get; set; }
    }
}
