using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Debugtime.Common.Persistence;
using Debugtime.Common.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Diagnostics;

[assembly: OwinStartup(typeof(Debugtime.Services.ServiceStartup))]

namespace Debugtime.Services
{
    public class ServiceStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Creat);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.CreatePerOwinContext<AppSignInManager>(AppSignInManager.Create);
            app.CreatePerOwinContext(() => new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext())));
            
            app.UseWelcomePage("/");
        }
    }
}
