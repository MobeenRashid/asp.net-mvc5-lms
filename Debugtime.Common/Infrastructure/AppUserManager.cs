using System.Threading.Tasks;
using Debugtime.Common.Configurations;
using Debugtime.Common.Dtos;
using Debugtime.Common.Persistence;
using DebugTime.Domain.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Debugtime.Common.Wrappers;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using Debugtime.Common.Identity;

namespace Debugtime.Common.Infrastructure
{
    public class AppUserManager : UserManager<ApplicationUser>
    {
        private AutoMapperProfileConfiguration _autoMapperProfile;

        public AppUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
        }


        public AutoMapperProfileConfiguration AutoMapperProfile => _autoMapperProfile ?? (_autoMapperProfile = new AutoMapperProfileConfiguration());

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext owinContext)
        {

            var userManager = new AppUserManager(new UserStore<ApplicationUser>(owinContext.Get<ApplicationDbContext>()));

            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            userManager.PasswordValidator = new PasswordValidator()
            {
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonLetterOrDigit = true,
                RequiredLength = 5
            };

            userManager.EmailService = new AppEmailService();

            userManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(options.DataProtectionProvider?.Create("ASP.Net Identity"));

            return userManager;
        }

        public async Task<AppIdentityResultWrapper> AddUserAsync(UserRegisterDto userToRegister)
        {
            var preparedUser = PrepareUserForCreation(userToRegister);
            IdentityResult identityResult;
            var resultWrapper = new AppIdentityResultWrapper
            {
                ModelErrors = new ModelStateDictionary()
            };
            if (String.IsNullOrEmpty(userToRegister.Password))
            {
                identityResult = await base.CreateAsync(preparedUser);

            }
            else
            {
                identityResult = await base.CreateAsync(preparedUser, userToRegister.Password);

            }

            if (identityResult.Succeeded)
            {
                resultWrapper.Succeeded = identityResult.Succeeded;
                resultWrapper.UserId = preparedUser.Id;
                resultWrapper.UserName = preparedUser.UserName;
                return resultWrapper;
            }

            identityResult.Errors.ToList().ForEach(err => resultWrapper.ModelErrors.AddModelError("", err));
            return resultWrapper;
        }

        private ApplicationUser PrepareUserForCreation(UserRegisterDto userToRegister)
        {
            var appUser = AutoMapperProfile.EntityMapper.Map<UserRegisterDto, ApplicationUser>(userToRegister);

            appUser.UserProfile = new UserProfile
            {
                Id = appUser.Id,
                FirstName = userToRegister.FirstName,
                LastName = userToRegister.LastName
            };
            
            return appUser;
        }

        public async Task<bool> HasCourseAsync(string courseId,string userId)
        {
            if (string.IsNullOrEmpty(courseId))
                return false;

            var context = HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();

            return await context.UserCourses.AnyAsync(c => c.CourseId == courseId && c.UserId == userId);
        }
    }
}