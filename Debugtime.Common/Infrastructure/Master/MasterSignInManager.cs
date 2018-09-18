using System.Security.Claims;
using System.Threading.Tasks;
using DebugTime.Domain.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Debugtime.Common.Infrastructure.Master
{
    public class MasterSignInManager : SignInManager<ApplicationUser, string>
    {
        public MasterSignInManager(MasterUserManager userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }

        public override async Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            var userManager = (MasterUserManager)UserManager;
            return await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

        }

        public static MasterSignInManager Create(IdentityFactoryOptions<MasterSignInManager> options, IOwinContext owinContext)
        {
            var manager = new MasterSignInManager(owinContext.GetUserManager<MasterUserManager>(), owinContext.Authentication);
            return manager;
        }

        public async Task<SignInStatus> SignInWithPasswordAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            return await PasswordSignInAsync(userName, password, isPersistent, shouldLockout);
        }
    }
}