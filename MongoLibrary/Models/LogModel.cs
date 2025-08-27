using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoLibrary.Models
{
    public class LogModel
    {
        public bool isError { get; set; }
        public object data { get; set; }
    }
}
