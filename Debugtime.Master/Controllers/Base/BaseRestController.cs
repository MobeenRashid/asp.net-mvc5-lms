using System;
using System.Web.Mvc;
using System.Web.Routing;
using Debugtime.Common.Extentions;
using Debugtime.Common.Rest_Services.Concretes;
using Debugtime.Common.Rest_Services.Contracts;
using Debugtime.Master.Extensions;
using Debugtime.Master.Rest_Services.Concretes;
using Debugtime.Master.Rest_Services.Contracts;

namespace Debugtime.Master.Controllers.Base
{
    public class BaseRestController : Controller
    {
        protected IUserRestService UserRestService;
        protected ICourseRestService CourseRestService;
        protected IProfileRestService ProfileRestService;

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {

            UserRestService = new UserRestService(new AppHttpClient(requestContext.HttpContext.Request, "application/json"));

            CourseRestService = new CourseRestService(new AppHttpClientMaster(requestContext.HttpContext.Request, "application/json"));

            ProfileRestService = new ProfileRestService(new AppHttpClient(requestContext.HttpContext.Request, "application/json"));

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}