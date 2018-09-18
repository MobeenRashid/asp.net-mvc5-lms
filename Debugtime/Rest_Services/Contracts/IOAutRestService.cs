using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Debugtime.Common.Wrappers;

namespace Debugtime.Rest_Services.Contracts
{
    public interface IOAutRestService
    {
        Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistence);
        Task<AppIdentityResultWrapper> AddExternalLoginAsync(string userId, UserLoginInfo loginInfo);
    }
}