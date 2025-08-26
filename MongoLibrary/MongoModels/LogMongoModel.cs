using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoLibrary.MongoModels
{
    public class LogMongoModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string id { get; set; }
        public object data { get; set; }
        public bool isError { get; set; }
        [BsonElement("DateTime")]
        public DateTime dateT { get; set; }
    }
}
