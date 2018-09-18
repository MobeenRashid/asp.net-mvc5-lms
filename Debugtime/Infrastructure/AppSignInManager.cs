using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
//using Debugtime.Common.Infrastructure;
//using DebugTime.Domain.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Debugtime.Model;
using Debugtime.Common.Infrastructure;

namespace Debugtime.Managers
{
    //public class AppSignInManager : SignInManager<ApplicationUser, string>
    //{
    //    public AppSignInManager(AppUserManager userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
    //    {
    //    }

    //    public async override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
    //    {
    //        var userManager = (AppUserManager)UserManager;
    //        return await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

    //    }

    //    public static AppSignInManager Create(IdentityFactoryOptions<AppSignInManager> options, IOwinContext owinContext)
    //    {
    //        var manager = new AppSignInManager(owinContext.GetUserManager<AppUserManager>(), owinContext.Authentication);
    //        return manager;
    //    }
    //}
}