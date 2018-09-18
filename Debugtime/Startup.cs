using Debugtime.Common.Infrastructure;
using Debugtime.Common.Persistence;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using Owin.Security.Providers.LinkedIn;
using System;
using System.Configuration;
using DebugTime.Domain.Model;

[assembly: OwinStartup(typeof(Debugtime.Startup))]

namespace Debugtime
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Creat);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.CreatePerOwinContext<AppSignInManager>(AppSignInManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                LoginPath = new PathString("/security/account/signin"),
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<AppUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => manager.CreateIdentityAsync(user, authenticationType: DefaultAuthenticationTypes.ApplicationCookie))
                }
            });


            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseFacebookAuthentication("130538950869778", "81c9958c878492482d21a41b996fd6ae");

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "935242653071-8rnu5go892edcsptf6ekm3nukvde31ai.apps.googleusercontent.com",
                ClientSecret = "0-x_j81xcMcytDx867GBbnQM"
            });

            app.UseLinkedInAuthentication(
                ConfigurationManager.AppSettings["LinkedInClientId"].ToString(),
                ConfigurationManager.AppSettings["LinkedInClientSecret"].ToString()
                );
        }
    }
}
