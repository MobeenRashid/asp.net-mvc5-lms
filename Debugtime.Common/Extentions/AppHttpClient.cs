using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Debugtime.Common.Static;

namespace Debugtime.Common.Extentions
{
    public class AppHttpClient:HttpClient
    {
        public AppHttpClient(HttpRequestBase requestContext,string mediaType)
        {
            //BaseAddress = new Uri($"{requestContext.Url?.Scheme}://{requestContext.Url?.Authority}");
            BaseAddress = new Uri(ResourceStrings.ApiBaseAddress);
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
        }
    }
}