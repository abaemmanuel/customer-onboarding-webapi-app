using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.Dtos
{
    public class Response<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool IsSuccessFul { get; set; }
        public HttpStatusCode ResponseCode { get; set; }
    }
}
