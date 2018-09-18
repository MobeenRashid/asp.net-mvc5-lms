using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Debugtime.Areas.Security.Models.Input;
using Debugtime.Common.Infrastructure;
using Microsoft.AspNet.Identity.Owin;
using WebGrease.Css.Extensions;
using Debugtime.Controllers.Base;
using Debugtime.Areas.Security.OAuth;
using Microsoft.Owin.Security;
using System.Linq;
using Debugtime.Common.Model.Input;

namespace Debugtime.Areas.Security.Controllers
{
    [Authorize]
    public class AccountController : BaseRestController
    {
        private AppSignInManager _appSignInManager;
        private AppUserManager _appUserManager;

        public AppSignInManager AppSignInManager
        {
            get
            {
                return _appSignInManager ??
                    (_appSignInManager = HttpContext.GetOwinContext().Get<AppSignInManager>());
            }
        }

        public AppUserManager AppUserManager
        {
            get
            {
                return _appUserManager ??
                    (_appUserManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>());
            }
        }

        public IAuthenticationManager AppAuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult SignUp(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignUp(UserRegisterViewModel newUser, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(newUser);

                var responceResult = await UserRestService.RegisterMemberAsync(newUser);

                if (!responceResult.HttpResponseMessage.IsSuccessStatusCode)
                {
                    ViewBag.ReturnUrl = returnUrl;
                    AddModelErrorsToModelState(responceResult.ModelErrors);
                    return View(newUser);
                }

                HttpContext.GetOwinContext().Authentication.SignOut();

                var token = await UserRestService.GetEmailConfirmationToken(responceResult.UserId);

                var redirectUri = Url.Action("ConfirmEmail", "Account",
                    new {area = "Security", token = token, userId = responceResult.UserId},
                    protocol: Request.Url.Scheme);
                await UserRestService.SendEmailAsync(responceResult.UserId, "Account Verification",
                    $@"Please Confirm your email address by clicking <a href=""{redirectUri}"">here</a>");

                var signInStatus =
                    await AppSignInManager.SignInWithPasswordAsync(responceResult.UserName, newUser.Password, false,
                        false);

                switch (signInStatus)
                {
                    case SignInStatus.Failure:
                        return RedirectToAction("SingInFailed", "Oops");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("Verification", "Account");
                    default:
                        return RedirectToReturnUrl(returnUrl);
                }
            }
            catch (HttpAntiForgeryException)
            {
                return RedirectToAction("Error","Oops",new{Subject="Sign Up Failed",Message="Sorry we can't sign you upp for security reasons, pleas try again",HelpUrl=Url.Action("SignUp","Account",new{area="Security"}),UrlText="Try again"});
            }

        }

        public async Task<ActionResult> ConfirmEmail(string token, string userId)
        {
            if (String.IsNullOrEmpty(token) || String.IsNullOrEmpty(userId))
                return View("Error");

            var succeeded = await UserRestService.ConfirmEmailAsync(userId, token);
            return View(succeeded ? "ConfirmEmail" : "Error");
        }

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
            try
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
            catch (Exception)
            {
                throw;
            }
        }

        private RedirectResult RedirectToReturnUrl(string returnUrl)
        {
            returnUrl = String.IsNullOrEmpty(returnUrl)
                                    ? Url.Action("Index", "Home", new { area = "" })
                                    : returnUrl;
            return Redirect(returnUrl);
        }

        private ActionResult SignInFailed(UserLoginViewModel viewModel)
        {
            ModelState.AddModelError("", "Invalid Email or Password");
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult SignOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Home", new { area = "" });
        }


        public ActionResult Verification()
        {
            return Content("Verification is required");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SocialLogin(string providerName, string returnUrl)
        {
            var result = new OAuthResult(providerName, Url.Action("OAuthCallBack", "Account", new { ReturnUrl = returnUrl }));
            return result;
        }

        [AllowAnonymous]
        public async Task<ActionResult> OAuthCallBack(string returnUrl)
        {
            var loginInfo = AppAuthManager.GetExternalLoginInfo();
            if (loginInfo == null)
            {
                return RedirectToAction("SignIn");
            }

            //var signInStatus = await OAuthRestService.ExternalSignInAsync(loginInfo, false);
            var signInStatus = await AppSignInManager.ExternalSignInAsync(loginInfo, false);
            switch (signInStatus)
            {
                case SignInStatus.Success:
                    return RedirectToReturnUrl(returnUrl);

                case SignInStatus.Failure:
                default:
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;

                    var names = loginInfo.ExternalIdentity.Name.Split(null);

                    var inputModel = new OAuthLoginConfirmationInputModel
                    {
                        FirstName = names.ElementAtOrDefault(0),
                        LastName = names.ElementAtOrDefault(1),
                        Email = loginInfo.Email
                    };

                    return View("OAuthLoginConfirmation", inputModel);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> OAuthLoginConfirmation(OAuthLoginConfirmationInputModel inputModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var loginInfo = await AppAuthManager.GetExternalLoginInfoAsync();

                if (loginInfo == null)
                    return RedirectToAction("ExternalLoginFailed");

                var registerModel = new UserRegisterViewModel
                {
                    FirstName = inputModel.FirstName,
                    LastName = inputModel.LastName,
                    Email = inputModel.Email,
                };


                var responceResult = await UserRestService.RegisterMemberAsync(registerModel);
                //var responceResult = await AppUserManager.AddUserAsync(new UserRegisterDto()
                //{
                //    Email = inputModel.Email,
                //    FirstName = inputModel.FirstName,
                //    LastName = inputModel.LastName,
                //    UserName = inputModel.Email
                //});



                if (!responceResult.HttpResponseMessage.IsSuccessStatusCode)
                {
                    ViewBag.ReturnUrl = returnUrl;
                    //var errorsValues = responceResult.ModelErrors.Values;

                    //if(errorsValues.Count > 0)
                    //errorsValues.ForEach(v => v.Errors.ForEach(e => ModelState.AddModelError("", e.ErrorMessage)));

                    AddModelErrorsToModelState(responceResult.ModelErrors);
                    return View(inputModel);
                }


                //var loginResult = await OAuthRestService.AddExternalLoginAsync(responceResult.UserId, loginInfo.Login);
                var loginResult = await AppUserManager.AddLoginAsync(responceResult.UserId, loginInfo.Login);

                if (loginResult.Succeeded)
                {
                    //var signInDto = new UserSignInDto { UserEmail = inputModel.Email, UserName = responceResult.UserName, UserId = responceResult.UserId };

                    //var messageWrapper = await UserRestService.SignInMemberAsync(signInDto);

                    HttpContext.GetOwinContext().Authentication.SignOut();

                    var signInStatus = await AppSignInManager.ExternalSignInAsync(loginInfo, false);

                    if(signInStatus == SignInStatus.Success)
                       return RedirectToReturnUrl(returnUrl);

                   return RedirectToReturnUrl(Url.Action("SignInFailed", "Account", new { area = "Security" }));
                }

              
                AddModelErrorsToModelState(loginResult.Errors);
            }

            return View(inputModel);
        }
    }
}