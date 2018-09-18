using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Debugtime.Common.Infrastructure;
using Debugtime.Common.Infrastructure.Master;
using Debugtime.Common.Model.Input;
using Debugtime.Master.Controllers.Base;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebGrease.Css.Extensions;

namespace Debugtime.Master.Controllers
{
    [Authorize]
    public class AccountController : BaseRestController
    {
        private MasterSignInManager _appSignInManager;
        private MasterUserManager _appUserManager;

        public MasterSignInManager AppSignInManager
        {
            get
            {
                return _appSignInManager ??
                    (_appSignInManager = HttpContext.GetOwinContext().Get<MasterSignInManager>());
            }
        }

        public MasterUserManager AppUserManager
        {
            get
            {
                return _appUserManager ??
                    (_appUserManager = HttpContext.GetOwinContext().GetUserManager<MasterUserManager>());
            }
        }

        public IAuthenticationManager AppAuthManager => HttpContext.GetOwinContext().Authentication;

     

        private void AddModelErrorsToModelState(IEnumerable<string> modelErrors)
        {
            modelErrors.ForEach(err => ModelState.AddModelError("", err));
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult SignIn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignIn(UserLoginViewModel viewModel, string returnUrl)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var resultWrapper = await UserRestService.GetUserNameByEmailAsync(viewModel.Email);

            HttpContext.GetOwinContext().Authentication.SignOut();

            if (!resultWrapper.HttpResponseMessage.IsSuccessStatusCode)
                return SignInFailed(viewModel);

            var result = await AppSignInManager.SignInWithPasswordAsync(resultWrapper.UserName, viewModel.Password, viewModel.RemeberMe, false);

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToReturnUrl(returnUrl);
                default:
                    ViewBag.ReturnUrl = returnUrl;
                    return SignInFailed(viewModel);
            }
        }

        private RedirectResult RedirectToReturnUrl(string returnUrl)
        {
            returnUrl = String.IsNullOrEmpty(returnUrl)
                                    ? Url.Action("Index", "Panel", new { area = "" })
                                    : returnUrl;
            return Redirect(returnUrl);
        }

        private ActionResult SignInFailed(UserLoginViewModel viewModel)
        {
            ModelState.AddModelError("", @"Invalid Email or Password");
            return View("SignIn",viewModel);
        }


        [HttpGet]
        public ActionResult SignOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("SignIn", "Account");
        }


        public ActionResult Verification()
        {
            return Content("Verification is required");
        }
        
    }
}