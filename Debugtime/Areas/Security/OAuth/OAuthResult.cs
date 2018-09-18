using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Debugtime.Areas.Security.OAuth
{
    internal class OAuthResult : HttpUnauthorizedResult
    {
        public OAuthResult(string providerName, string returnUri)
            : this(providerName, returnUri, null)
        {

        }

        public OAuthResult(string providerName, string redirectUri, string userId)
        {
            ProviderName = providerName;
            RedirectUri = redirectUri;
            UserId = userId;
        }

        public string ProviderName { get; set; }
        public string RedirectUri { get; set; }
        public string UserId { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            var properties = new AuthenticationProperties { RedirectUri = RedirectUri };

            if (UserId != null)
            {
                properties.Dictionary["XsrfKey"] = UserId;
            }

            context.HttpContext.GetOwinContext().Authentication.Challenge(properties, ProviderName);
        }
    }
}