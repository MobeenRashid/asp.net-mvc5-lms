using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Debugtime.Common.Dtos;
using System.Threading.Tasks;
using Debugtime.Services.Controllers.Base;

namespace Debugtime.Services.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : BaseApiController
    {

        [Route("{userId}")]
        public async Task<IHttpActionResult> GetById(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            var user = await UnitOfUtility.UserUtility.GetUserWithProfileAsync(userId);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [Route("{email}/username")]
        public async Task<IHttpActionResult> GetUserNameByEmail(string email)
        {
            var user = await UnitOfUtility.UserUtility.GetUserByEmailAsync(email);
            if (user != null)
                return Ok(user.UserName);

            return BadRequest("User not found");
        }

        [HttpPost]
        [Route("update")]
        public async Task<IHttpActionResult> Updateuser(ApplicationUserDto userInfo)
        {
            if (userInfo == null)
                return BadRequest();

            var r = await UnitOfUtility.UserUtility.UpdateAsync(userInfo);
            if (r)
                return Ok();

            return NotFound();
        }

        [HttpPost]
        [Route("{userId}/email/ConfirmationToken")]
        public async Task<IHttpActionResult> GetUserEmailConfirmationToken(string userId)
        {
            var token = await UnitOfUtility.UserUtility.GenerateEmailConfirmationTokenAsync(userId);

            if (String.IsNullOrWhiteSpace(token))
                return Ok(token);

            return BadRequest("Failed to generate token ");
        }

        [HttpPost]
        [Route("SendEmail")]
        public async Task<IHttpActionResult> SendEmail(EmailDto emailDto)
        {
            var isDone = await UnitOfUtility.UserUtility.SendEmailAsync(emailDto.UserId, emailDto.EmailSubject, emailDto.EmailBody);

            if (isDone)
                return Ok();

            return BadRequest("Failed to Send email");
        }

        [HttpPost, Route("ConfirmEmail/{userId}/{userToken}")]
        public async Task<IHttpActionResult> ConfirmEmail(string userId, string userToken)
        {
            var result = await UnitOfUtility.UserUtility.ConfirmEmailAsync(userId, userToken);
            if (result.Succeeded)
                return Ok("Email Confirmed");

            return BadRequest("Failed to Confirm Email");
        }

        [HttpGet]
        [Route("roles")]
        public IHttpActionResult GetRole()
        {
            var roles = UnitOfUtility.UserUtility.GetRoles();

            if (roles != null && roles.Any())
                return Ok(roles);

            return NotFound();
        }


        [HttpGet]
        [Route("summary")]
        public async Task<IHttpActionResult> GetUserssummary()
        {
            //var users = await UnitOfUtility.ProfileUtility.GetUsageSummaryAsync();

            //if (users.TotalUsers!=0)
            //    return Ok(users);

            return NotFound();
        }

        [HttpGet]
        [Route("list")]
        public async Task<IHttpActionResult> GetUsersLis()
        {
            var users = await UnitOfUtility.ProfileUtility.GetProfileListAsync();

            if (users.Any())
                return Ok(users);

            return NotFound();
        }

        [HttpGet]
        [Route("{userId}/Delete")]
        public async Task<IHttpActionResult> DeleteUser(string userId)
        {
            if (String.IsNullOrEmpty(userId))
                return BadRequest();

            var r = await UnitOfUtility.UserUtility.DeleteAsync(userId);

            if (r)
                return Ok();

            return NotFound();
        }
    }
}