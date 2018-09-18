using Debugtime.Rest_Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Debugtime.Extentions;
using Debugtime.Common.Dtos;
using System.Net.Http;
using Debugtime.Common.Extentions;
using Debugtime.Common.Wrappers;

namespace Debugtime.Rest_Services.Concretes
{
    public class OAuthRestService : IOAutRestService
    {
        private readonly AppHttpClient _httClient;
        public OAuthRestService(AppHttpClient httpClient)
        {
            _httClient = httpClient;
        }

        public async Task<AppIdentityResultWrapper> AddExternalLoginAsync(string userId, UserLoginInfo loginInfo)
        {
            var loginDto = new ExternalLoginDto
            {
                UserId = userId,
                LoginInfo = loginInfo
            };

            var responceResult = await _httClient.PostAsJsonAsync<ExternalLoginDto>("api/account/external/login", loginDto);

            var identityResult=
                await responceResult.Content.ReadAsAsync<AppIdentityResultWrapper>();
        
            return identityResult;

        }

        public async Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistence)
        {
            var signInDto = new ExternalSignInDto
            {
                LoginInfo = loginInfo,
                IsPersistence = isPersistence
            };

            var responceResult = await _httClient.PostAsJsonAsync<ExternalSignInDto>("api/account/external/signin", signInDto);

            return await responceResult.Content.ReadAsAsync<SignInStatus>();
        }
    }
}