using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperGeneral.Models
{
    public class ResponseData<T>
    {
        public bool isTrue {  get; set; }
        public T? data { get; set; }
        public string message { get; set; }
        public string error { get; set; }

        public ResponseData()
        {
            isTrue = true;
            this.data = default;
            message = string.Empty;
            error = string.Empty;
        }

        public ResponseData(T data)
        {
            isTrue = true;
            this.data = data;
            message = string.Empty;
            error = string.Empty;
        }

        public ResponseData(string message, string error)
        {
            isTrue = false;
            data = default;
            this.message = message;
            this.error = error;
        }
    }
}
