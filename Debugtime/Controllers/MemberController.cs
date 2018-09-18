using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Debugtime.Controllers.Base;
using System.Threading.Tasks;
using Debugtime.Common.Configurations;
using System.Web;
using Debugtime.Common.Model.Input;
using Debugtime.Helpers;

namespace Debugtime.Controllers
{
    [Authorize]
    [RoutePrefix("member")]
    public class MemberController : BaseRestController
    {
        private AutoMapperProfileConfiguration _mapperConfig;

        public AutoMapperProfileConfiguration MapperConfig => _mapperConfig ?? (_mapperConfig = new AutoMapperProfileConfiguration());



        [Route("{username}")]
        [OutputCache(Duration = 10, VaryByParam = "userName")]
        public async Task<ActionResult> Index(string userName)
        {
            var profile = await ProfilesRestService.GetProfileToViewByUserNameAsync(userName);
            return View(profile);
        }

        [HttpGet]
        [Route("edit")]
        public async Task<ActionResult> Edit()
        {
            ViewBag.Success = false;
            try
            {
                var userId = User.Identity.GetUserId();
                var userInputModel = await ProfilesRestService.GetProfileToEditByIdAsync(userId);

                if (userInputModel != null)
                    return View(userInputModel);

                return RedirectToAction("Error", "Oops", new { Subject="User Not Found"});
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Oops", new
                {
                    Subject = "Internal Server Error"
                });
            }

        }


        [Route("edit")]
        [HttpPost]
        [ValidateInput(true)]
        public async Task<ActionResult> Edit(UserProfileInputModel profileInfo)
        {
            ViewBag.Success = false;

            try
            {
                if (!ModelState.IsValid)
                    return View(profileInfo);

                profileInfo.UserAccountId = User.Identity.GetUserId();
                var responceMessage = await ProfilesRestService.SaveInfoAsync(profileInfo);

                if (responceMessage.HttpResponseMessage.IsSuccessStatusCode)
                    ViewBag.Success = true;

                return View("Edit", profileInfo);
            }
            catch (Exception)
            {
                return Content("Exception Occured and Its Handeled :D");
            }
        }

        [HttpPost]
        [Route("UploadAvatar")]
        public async Task<ActionResult> UploadAvatar(HttpPostedFileBase userAvatar)
        {
            try
            {
                var result = await ProfilesRestService.SaveAvatarAsync(userAvatar);

                if (result.HttpResponseMessage.IsSuccessStatusCode)
                    return RedirectToAction("Edit");

                return RedirectToAction("Error", "Oops", new { Subject = "Upload Avatar Failed", Message = "we recieved a bad request, it might occur because of inccorect data.", HelpUrl = Url.Action("Edit", "Member") });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Oops");
            }

        }

        [ChildActionOnly]
        [Route("GetFirstName")]
        public async Task<string> GetFirstName()
        {
            return await UserHelper.GetFirstName();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{userId}/avatar")]
        public async Task<FileContentResult> GetAvatar(string userId)
        {
            var avatar = await ProfilesRestService.GetAvatarAsync(userId);
            if (avatar != null)
                return File(avatar.Content, avatar.ContentType);

            string path = HttpContext.Server.MapPath("~/Content/Images/no-image.png");
            var content = System.IO.File.ReadAllBytes(path);

            return File(content, "image/png");
        }
    }
}