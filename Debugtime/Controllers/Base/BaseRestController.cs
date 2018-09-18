using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Debugtime.Common.Extentions;
using Debugtime.Common.Rest_Services.Concretes;
using Debugtime.Common.Rest_Services.Contracts;
using Debugtime.Extentions;
using Debugtime.Helpers;
using Debugtime.Rest_Services.Concretes;
using Debugtime.Rest_Services.Contracts;

namespace Debugtime.Controllers.Base
{
    public class BaseRestController : Controller
    {
        protected IUserRestService UserRestService;
        protected IProfileRestService ProfilesRestService;
        protected IOAutRestService OAuthRestService;
      
        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {


            UserRestService = new UserRestService(new AppHttpClient(requestContext.HttpContext.Request, "application/json"));

            ProfilesRestService = new ProfileRestService(new AppHttpClient(requestContext.HttpContext.Request, "application/json"));

            OAuthRestService = new OAuthRestService(new AppHttpClient(requestContext.HttpContext.Request, "application/json"));
            return base.BeginExecute(requestContext, callback, state);

        }
    }
}