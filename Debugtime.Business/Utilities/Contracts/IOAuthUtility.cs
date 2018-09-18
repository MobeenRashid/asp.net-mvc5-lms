using Debugtime.Common.Dtos;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugtime.Business.Utilities.Contracts
{
    public interface IOAuthUtility
    {
        Task<SignInStatus> SignInExternalyAsync(ExternalLoginInfo loginInfo, bool isPersistence);
        Task<IdentityResult> AddExternalLoginAsync(string userId, UserLoginInfo loginInfo);

    }
}
