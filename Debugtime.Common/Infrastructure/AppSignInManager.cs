using System.Security.Claims;
using System.Threading.Tasks;
using DebugTime.Domain.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Debugtime.Common.Infrastructure
{
    public class AppSignInManager : SignInManager<ApplicationUser, string>
    {
        public AppSignInManager(AppUserManager userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }

        public override async Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            var userManager = (AppUserManager)UserManager;
            return await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

        }

        public static AppSignInManager Create(IdentityFactoryOptions<AppSignInManager> options, IOwinContext owinContext)
        {
            var manager = new AppSignInManager(owinContext.GetUserManager<AppUserManager>(), owinContext.Authentication);
            return manager;
        }

        public async Task<SignInStatus> SignInWithPasswordAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            return await PasswordSignInAsync(userName, password, isPersistent, shouldLockout);
        }
    }
}