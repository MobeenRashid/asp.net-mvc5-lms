using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;
using Debugtime.Common.Configurations;
using Debugtime.Common.Dtos;
using Debugtime.Common.Identity;
using Debugtime.Common.Persistence;
using Debugtime.Common.Wrappers;
using DebugTime.Domain.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Debugtime.Common.Infrastructure.Master
{
    public class MasterUserManager : UserManager<ApplicationUser>
    {
        private AutoMapperProfileConfiguration _autoMapperProfile;

        public MasterUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
        }


        public AutoMapperProfileConfiguration AutoMapperProfile
        {
            get { return _autoMapperProfile ?? (_autoMapperProfile = new AutoMapperProfileConfiguration()); }
        }

        public static MasterUserManager Create(IdentityFactoryOptions<MasterUserManager> options, IOwinContext owinContext)
        {

            var userManager = new MasterUserManager(new UserStore<ApplicationUser>(owinContext.Get<ApplicationDbContext>()));

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
    }
}