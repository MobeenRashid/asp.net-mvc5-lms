using Debugtime.Rest_Services.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Debugtime.Common.Extentions;
using Debugtime.Common.Infrastructure;
using Debugtime.Common.Model.View;
using Debugtime.Common.Rest_Services.Concretes;
using Microsoft.AspNet.Identity.Owin;
using Debugtime.Extentions;

namespace Debugtime.Helpers
{
    public static class UserHelper
    {
        public async static Task<string> GetFirstName()
        {
            AppUserManager manager = HttpContext.Current.GetOwinContext().Get<AppUserManager>();

            var httpClient = new AppHttpClient(HttpContext.Current.Request.RequestContext.HttpContext.Request, "application/json");
            var restService = new ProfileRestService(httpClient);

            var userName = await restService.GetFirstNameAsync(HttpContext.Current.User.Identity.GetUserId());
            return userName;
        }

        public static bool IsShortBioNull(UserProfileViewModel model)
        {
            return (String.IsNullOrEmpty(model.ShortBio));
        }

        public static bool IsWebsiteLinkNull(UserProfileViewModel model)
        {
            return (String.IsNullOrEmpty(model.Website));
        }
        public static bool IsFacebookLinkNull(UserProfileViewModel model)
        {
            return (String.IsNullOrEmpty(model.Facebook));
        }
        public static bool IsLinkedInLinkNull(UserProfileViewModel model)
        {
            return (String.IsNullOrEmpty(model.LinkedIn));
        }
        public static bool IsGooglePlusLinkNull(UserProfileViewModel model)
        {
            return (String.IsNullOrEmpty(model.GooglePlus));
        }
        public static bool IsAllSocialLinksNull(UserProfileViewModel model)
        {
            return (String.IsNullOrEmpty(model.Facebook) && String.IsNullOrEmpty(model.GooglePlus) && String.IsNullOrEmpty(model.LinkedIn) && String.IsNullOrEmpty(model.Website));
        }
    }
}