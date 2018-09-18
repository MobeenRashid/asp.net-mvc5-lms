using Debugtime.Common.Infrastructure.Master;
using Debugtime.Common.Persistence;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Debugtime.Master.MasterStartup))]

namespace Debugtime.Master
{
    public class MasterStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Creat);
            app.CreatePerOwinContext<MasterUserManager>(MasterUserManager.Create);
            app.CreatePerOwinContext<MasterSignInManager>(MasterSignInManager.Create);

            app.CreatePerOwinContext(() => new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext())));
            
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/account/signin")
            });
        }
    }
}
