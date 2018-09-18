using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Debugtime.Common.Dtos;
using Debugtime.Common.Wrappers;
using Debugtime.Services.Controllers.Base;

namespace Debugtime.Services.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : BaseApiController
    {

        [HttpPost]
        [AllowAnonymous]
        [Route("signUp")]
        public async Task<IHttpActionResult> Register(UserRegisterDto newUser)
        {
            try
            {
                var resultWrapper = await UnitOfUtility.UserUtility.AddUserAsync(newUser);

                if (!resultWrapper.Succeeded)
                    return BadRequest(resultWrapper.ModelErrors);

                var idAndName = new UserIdAndName
                {
                    UserId = resultWrapper.UserId,
                    UserName = resultWrapper.UserName
                };

                return Ok(idAndName);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        [HttpPost, AllowAnonymous]
        [Route("SignIn/User")]
        public async Task<IHttpActionResult> SignInAsync(UserSignInDto userInfo)
        {
            var result = await UnitOfUtility.UserUtility.AddSignInAsync(userInfo);

            if (result)
                return Ok("Success");

            return StatusCode(System.Net.HttpStatusCode.ExpectationFailed);
        }

        [HttpPost, AllowAnonymous]
        [Route("external/signin")]
        public async Task<IHttpActionResult> UserSignInAsync(ExternalSignInDto signInDto)
        {
            var result = await UnitOfUtility.OAuthUtility.SignInExternalyAsync(signInDto.LoginInfo, signInDto.IsPersistence);

            return Ok(result);
        }

        [HttpPost, AllowAnonymous]
        [Route("external/login")]
        public async Task<IHttpActionResult> AddExternalLogin(ExternalLoginDto logInDto)
        {
            var result = await UnitOfUtility.OAuthUtility.AddExternalLoginAsync(logInDto.UserId, logInDto.LoginInfo);

            var resultWrapper = new AppIdentityResultWrapper
            {
                Succeeded = result.Succeeded
            };

          
            if (result.Succeeded)
                return Ok(resultWrapper);

            result.Errors.ToList().ForEach(err => resultWrapper.ModelErrors.AddModelError("", err));

            return BadRequest(resultWrapper.ModelErrors);
        }
    }

}
