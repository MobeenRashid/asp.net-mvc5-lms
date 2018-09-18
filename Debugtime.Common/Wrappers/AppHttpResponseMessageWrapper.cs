using System.Collections.Generic;
using System.Net.Http;

namespace Debugtime.Common.Wrappers
{
    public class AppHttpResponseMessageWrapper
    {
        public HttpResponseMessage HttpResponseMessage;

        public AppHttpResponseMessageWrapper(HttpResponseMessage httpResponseMessage)
        {
            HttpResponseMessage = httpResponseMessage;
        }

        public List<string> ModelErrors { get; set; }
        public string ErrorMessage { get; set; } = "Oops, an error ocurred while processing your request";
        public string UserName { get; set; }
        public string UserId { get; set; }
    }
}