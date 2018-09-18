using Debugtime.Business.Utilities.Contracts;
using Debugtime.Common.Dtos;
using Debugtime.Common.Infrastructure;
using DebugTime.Domain.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Debugtime.Business.Utilities.Concretes
{
    public class OAuthUtility : IOAuthUtility
    {
        private AppSignInManager _signInManager;
        private AppUserManager _userManager;

        public AppSignInManager AppSignInManager
        {
            get
            {
                return _signInManager ?? (_signInManager = HttpContext.Current.GetOwinContext().Get<AppSignInManager>());
            }
        }

        public AppUserManager AppUserManager
        {
            get
            {
                return _userManager ?? (_userManager = HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>());
            }

        }

        public async Task<SignInStatus> SignInExternalyAsync(ExternalLoginInfo loginInfo, bool isPersistence)
        {
            try
            {
                return await AppSignInManager.ExternalSignInAsync(loginInfo, isPersistence);
            }
            catch (Exception)
            {
                return SignInStatus.Failure;
            }
        }

        public async Task<IdentityResult> AddExternalLoginAsync(string userId, UserLoginInfo loginInfo)
        {
            try
            {
                return await AppUserManager.AddLoginAsync(userId, loginInfo);
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<bool> SignInUserAsync(UserSignInDto user)
        {
            try
            {
                var appUser = new ApplicationUser
                {
                    Id = user.UserId,
                    UserName = user.UserName,
                    Email = user.UserEmail
                };

                await AppSignInManager.SignInAsync(appUser, user.IsPersistence, user.RememberBrowser);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
