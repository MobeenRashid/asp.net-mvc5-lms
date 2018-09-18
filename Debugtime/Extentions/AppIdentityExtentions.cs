using Debugtime.Rest_Services.Concretes;
using Debugtime.Rest_Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Debugtime.Common.Extentions;
using Debugtime.Common.Rest_Services.Concretes;

namespace Debugtime.Extentions
{
    public static class AppIdentityExtentions
    {

        public static string GetFullName(this IIdentity principle)
        {
            var profileRestService = new ProfileRestService(new AppHttpClient(HttpContext.Current.Request.RequestContext.HttpContext.Request, "application/json"));

            var userId = HttpContext.Current.User.Identity.GetUserId();
            var task = profileRestService.GetFullNameAsync(userId);
            task.Wait();
            return task.Result;
        }

        public async static Task<string> GetFirstName(this IIdentity principle)
        {
            var profileRestService = new ProfileRestService(new AppHttpClient(HttpContext.Current.Request.RequestContext.HttpContext.Request, "application/json"));

            var userId = HttpContext.Current.User.Identity.GetUserId();
            return await profileRestService.GetFirstNameAsync(userId);
        }
    }
}