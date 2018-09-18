using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Debugtime.Common.Configurations;
using Debugtime.Common.Model.Input;
using Debugtime.Master.Controllers.Base;
using Microsoft.AspNet.Identity;

namespace Debugtime.Master.Controllers
{
    [Authorize]
    [RoutePrefix("member")]
    public class MemberController : BaseRestController
    {
        private AutoMapperProfileConfiguration _mapperConfig;

        public AutoMapperProfileConfiguration MapperConfig => _mapperConfig ?? (_mapperConfig = new AutoMapperProfileConfiguration());


        //[ChildActionOnly]
        //[Route("GetFirstName")]
        //public async Task<string> GetFirstName()
        //{
        //    return await UserHelper.GetFirstName();
        //}

        [HttpGet]
        [AllowAnonymous]
        [Route("{userId}/avatar")]
        public async Task<FileContentResult> GetAvatar(string userId)
        {
            var avatar = await ProfileRestService.GetAvatarAsync(userId);
            if (avatar != null)
                return File(avatar.Content, avatar.ContentType);

            string path = HttpContext.Server.MapPath("~/Content/Images/no-image.png");
            var content = System.IO.File.ReadAllBytes(path);

            return File(content, "image/png");
        }
    }
}
