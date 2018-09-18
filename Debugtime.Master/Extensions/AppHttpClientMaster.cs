using Debugtime.Common.Static;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Debugtime.Master.Extensions
{
    public class AppHttpClientMaster:HttpClient
    {
        public AppHttpClientMaster(HttpRequestBase requestContext, string mediaType)
        {
            BaseAddress = new Uri(ResourceStrings.ApiBaseAddress);
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
        }

    }
}