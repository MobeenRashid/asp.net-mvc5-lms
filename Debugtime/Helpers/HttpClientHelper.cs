using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Debugtime.Helpers
{
    public class HttpClientHelper:HttpClient
    {
        public HttpClientHelper():base()
        {
        }

        public void PrepareClient(HttpRequestBase request, string acceptHeader)
        {
            this.BaseAddress = new Uri($"{request.Url?.Scheme}://{request.Url?.Authority}");
            this.DefaultRequestHeaders.Accept.
                Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}