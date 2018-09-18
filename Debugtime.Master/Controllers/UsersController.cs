using Debugtime.Common.Infrastructure;
using Debugtime.Common.Model.Input;
using Debugtime.Master.Controllers.Base;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Debugtime.Common.Infrastructure.Master;
using Debugtime.Common.Rest_Services.Concretes;

namespace Debugtime.Master.Controllers
{
    [Authorize]
    public class UsersController : BaseRestController
    {
        private MasterSignInManager _appSignInManager;
        private MasterUserManager _appUserManager;

        public MasterSignInManager AppSignInManager
        {
            get
            {
                if (_appSignInManager != null)
                    return _appSignInManager;

                _appSignInManager = HttpContext.GetOwinContext()
                    .Get<MasterSignInManager>();
                return _appSignInManager;
            }
        }

        public MasterUserManager AppUserManager
        {
            get
            {
                if (_appUserManager != null)
                    return _appUserManager;

                _appUserManager = HttpContext.GetOwinContext().GetUserManager<MasterUserManager>();

                return _appUserManager;
            }
        }

        public IAuthenticationManager AppAuthManager => HttpContext.GetOwinContext().Authentication;



        private void AddModelErrorsToModelState(IEnumerable<string> modelErrors)
        {
            modelErrors.ForEach(err => ModelState.AddModelError("", err));
        }

        [HttpGet]
        public async Task<ActionResult> Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            var users = await UserRestService.GetUsersLisAsync();

            return View(users);
        }

        [HttpGet]
        public async Task<ActionResult> Add(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            var roles = await UserRestService.GetAllRoles();
            if (roles!=null)
            {
                ViewBag.Roles = roles.Select(r => new SelectListItem { Value = r.RoleName, Text = r.RoleName });
            }
            ViewBag.Roles = new List<SelectListItem>();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(UserRegisterViewModel newUser, string returnUrl, string role)
        {
            var roles = await UserRestService.GetAllRoles();
            ViewBag.Roles = roles.Select(r => new SelectListItem { Value = r.RoleName, Text = r.RoleName });

            if (!ModelState.IsValid)
                return View(newUser);

            var responceResult = await UserRestService.RegisterMemberAsync(newUser);

            if (!responceResult.HttpResponseMessage.IsSuccessStatusCode)
            {
                ViewBag.ReturnUrl = returnUrl;
                AddModelErrorsToModelState(responceResult.ModelErrors);
                return View(newUser);
            }

            await AppUserManager.AddToRoleAsync(responceResult.UserId, role);

            return RedirectToReturnUrl(returnUrl);
        }

        private RedirectResult RedirectToReturnUrl(string returnUrl)
        {
            returnUrl = String.IsNullOrEmpty(returnUrl)
                                    ? Url.Action("Index", "Users", new { area = "" })
                                    : returnUrl;
            return Redirect(returnUrl);
        }

        [HttpGet]
        public new ActionResult Profile()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Author_Profile()
        {
            return View();
        }

        [HttpGet]
        [Route("users/{userId}/avatar")]
        public async Task<FileContentResult> GetAvatar(string userId)
        {
            var avatar = await ProfileRestService.GetAvatarAsync(userId);
            if (avatar != null)
                return File(avatar.Content, avatar.ContentType);

            var path = HttpContext.Server.MapPath("~/Content/Images/no-image.png");

            var content = System.IO.File.ReadAllBytes(path);

            return File(content, "image/png");
        }

        public async Task<ActionResult> Edit(string id)
        {
            var user = await UserRestService.GetUserAsync(id);

            var userRoles = await AppUserManager.GetRolesAsync(user.Id);
            var roles = await UserRestService.GetAllRoles();
            ViewBag.Roles = roles.Select(r =>
                {
                    var item = new SelectListItem { Value = r.RoleName, Text = r.RoleName };
                    var firstOrDefault = userRoles.FirstOrDefault();

                    if (firstOrDefault != null && firstOrDefault.Equals(r.RoleName))
                        item.Selected = true;
                    return item;
                }
            );

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserEditInputModel userInfo, string role)
        {
            var userRoles = await AppUserManager.GetRolesAsync(userInfo.Id);
            var roles = await UserRestService.GetAllRoles();
            ViewBag.Roles = roles.Select(r =>
                {
                    var item = new SelectListItem { Value = r.RoleName, Text = r.RoleName };
                    var firstOrDefault = userRoles.FirstOrDefault();

                    if (firstOrDefault != null && firstOrDefault.Equals(r.RoleName))
                        item.Selected = true;
                    return item;
                }
            );

            if (!ModelState.IsValid)
                return View(userInfo);

            var result = await UserRestService.UpdateUserAsync(userInfo);

            if (result)
            {
                await AppUserManager.AddToRoleAsync(userInfo.Id, role);
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", @"User you are trying to update not exist");
            return View(userInfo);
        }
    }
}