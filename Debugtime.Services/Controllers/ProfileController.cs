using Debugtime.Business.Utilities.Concretes;
using Debugtime.Common.Dtos;
using System;
using System.Net;
using System.Web.Http;
using Debugtime.Business.Utilities.Contracts;
using System.Threading.Tasks;
using Debugtime.Services.Controllers.Base;

namespace Debugtime.Services.Controllers
{
    [RoutePrefix("api/profile")]
    public class ProfileController : BaseApiController
    {

        [Route("{userId}/edit")]
        public async Task<IHttpActionResult> GetUserProfileToEdit(string userId)
        {
            UserProfileInputDto profileInputDto = 
                await UnitOfUtility.ProfileUtility.GetProfileToEditByIdAsync(userId);
            if (profileInputDto == null)
                return BadRequest("User Not Found");

            return Ok(profileInputDto);
        }

        [Route("{userName}/view")]
        public async Task<IHttpActionResult> GetUserProfileToView(string userName)
        {
            var profileDto = 
                await UnitOfUtility.ProfileUtility.GetProfileToViewByUserNameAsync(userName);

            if (profileDto == null)
                return BadRequest("User Not Found");

            return Ok<UserProfileViewDto>(profileDto);
        }

        [Route("save")]
        public async Task<IHttpActionResult> SaveInfo(UserProfileInputDto profileInfo)
        {
            try
            {
                var result = await UnitOfUtility.ProfileUtility.UpdateProfileAsync(profileInfo);

                if (result == true)
                    return Ok();

                return StatusCode(HttpStatusCode.NotModified);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [Route("avatar/upload")]
        public async Task<IHttpActionResult> UploadAvatar(UserAvatarDto modelInfo)
        {
            var result = await UnitOfUtility.ProfileUtility.UploadAvatarInfoAsync(modelInfo);

            if (result == true)
                return Ok("File Uploaded");

            return BadRequest("File upload failed");
        }

        [HttpGet]
        [Route("{id}/avatar")]
        public async Task<UserAvatarDto> GetAvatar(string id)
        {
            return await UnitOfUtility.ProfileUtility.GetAvatarByIdAsync(id);

            //var response = new HttpResponseMessage();

            //if (avatarInfo == null || avatarInfo.Content.Length == 0)
            //{
            //    response.StatusCode = HttpStatusCode.NotFound;

                //HttpServerUtility serverUtility = new HttpServerUtility()
                //var path= @"~\debugtime\Debugtime.Services\Content\Images\no - image.png";
                ////FileInfo info = new FileInfo();

                //response.Content = new ByteArrayContent(File.ReadAllBytes(path));
                //response.Content.Headers.ContentType = new MediaTypeHeaderValue(File.)
            //}


            //response.StatusCode = HttpStatusCode.OK;
            //response.Content = new ByteArrayContent(avatarInfo.Content);
            //response.Content.Headers.ContentType = new MediaTypeHeaderValue(avatarInfo.ContentType);
            //return response;
        }


        [HttpGet]
        [Route("{userId}/fullName")]
        public async Task<IHttpActionResult> GetFullName(string userId)
        {
            var fullName = await UnitOfUtility.ProfileUtility.GetFullNameByIdAsync(userId);
            if (String.IsNullOrEmpty(fullName))
                return BadRequest("Full Name Not Found");
            return Ok(fullName);
        }

        [HttpGet]
        [Route("{userId}/firstName")]
        public async Task<IHttpActionResult> GetFirstName(string userId)
        {
            var firstName=await UnitOfUtility.ProfileUtility.GetFirstNameByIdAsync(userId);

            if (String.IsNullOrEmpty(firstName))
                return BadRequest("First Name Not Found");
            return Ok(firstName);
        }
    }
}
