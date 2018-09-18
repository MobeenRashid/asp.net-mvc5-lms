using System.Threading.Tasks;
using System.Web;
using Debugtime.Business.Helpers;
using Debugtime.Business.Utilities.Contracts;
using Debugtime.Common.Dtos;
using Debugtime.Common.Infrastructure;
using Debugtime.DataAccess.Core.IRepositories;
using Microsoft.AspNet.Identity.Owin;
using Debugtime.DataAccess.Persistence.Repositories;
using Debugtime.Common.Wrappers;
using DebugTime.Domain.Model;
using Debugtime.Common.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Debugtime.Business.Utilities.Concretes
{
    public class UserUtility : AutoMapperProfileConfiguration, IUserUtility
    {
        private readonly IUnitOfWork _unitOfWork;
        private AppUserManager _userManager;
        private readonly UserHelper _userHelper;
        private AppSignInManager _signInManager;
        public UserUtility()
        {
            _unitOfWork = new UnitOfWork();
            _userHelper = new UserHelper(_unitOfWork);
        }

        public AppSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? (_signInManager = HttpContext.Current.GetOwinContext().Get<AppSignInManager>());
            }
        }

        public AppUserManager AppUserManager
        {
            get
            {
                return _userManager ?? (_userManager = HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>());
            }
        }

        public IAuthenticationManager AppAuthManager
        {
            get { return HttpContext.Current.GetOwinContext().Authentication; }
        }

        public async Task<ApplicationUserDto> GetUserByIdAsync(string id)
        {
            var appUser = await _unitOfWork.UsersRepository.GetSingleAsync(ur => ur.Id == id);
            return EntityMapper.Map<ApplicationUser, ApplicationUserDto>(appUser);
        }

        public async Task<ApplicationUserDto> GetUserWithProfileAsync(string userId)
        {
            var user = await _unitOfWork.UsersRepository.GetSingleAsync(u => u.Id == userId, u => u.UserProfile);

            var userDto = EntityMapper.Map<ApplicationUser, ApplicationUserDto>(user);
            userDto = EntityMapper.Map<UserProfile, ApplicationUserDto>(user.UserProfile, userDto);

            return userDto;
        }

        public async Task<bool> UpdateAsync(ApplicationUserDto userInfo)
        {
            try
            {
                if (userInfo == null)
                    return false;

                var appUser = EntityMapper.Map<ApplicationUserDto, ApplicationUser>(userInfo);
                appUser.UserProfile = EntityMapper.Map<ApplicationUserDto, UserProfile>(userInfo);

                _unitOfWork.UsersRepository.UpdateUser(appUser);

                var r = await _unitOfWork.SaveWorkAsync();

                return r > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ApplicationUserDto> GetUserByEmailAsync(string email)
        {
            var appUser = await _unitOfWork.UsersRepository.GetSingleAsync(ur => ur.Email == email);
            return EntityMapper.Map<ApplicationUser, ApplicationUserDto>(appUser);
        }

        public async Task<AppIdentityResultWrapper> AddUserAsync(UserRegisterDto newUser)
        {
            var fixedUser = _userHelper.FixUser(newUser);

            return await AppUserManager.AddUserAsync(fixedUser);

        }

        public async Task<bool> AddSignInAsync(UserSignInDto user)
        {
            try
            {
                var appUser = new ApplicationUser
                {
                    Id = user.UserId,
                    UserName = user.UserName,
                    Email = user.UserEmail
                };

                await SignInManager.SignInAsync(appUser, user.IsPersistence, user.RememberBrowser);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(string userId)
        {
            try
            {
                return await AppUserManager.GenerateEmailConfirmationTokenAsync(userId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> SendEmailAsync(string userId, string emailSubject, string emailMessage)
        {
            try
            {
                await AppUserManager.SendEmailAsync(userId, emailSubject, emailMessage);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
        {
            try
            {
                return await AppUserManager.ConfirmEmailAsync(userId, token);
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistence)
        {
            try
            {
                return await SignInManager.ExternalSignInAsync(loginInfo, isPersistence);
            }
            catch (Exception)
            {
                return SignInStatus.Failure;
            }
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo loginInfo)
        {
            try
            {
                return await AppUserManager.AddLoginAsync(userId, loginInfo);
            }
            catch (Exception)
            {
                return null;
            }

        }

        public IList<RoleDto> GetRoles()
        {
            try
            {
                var roleManager = HttpContext.Current.GetOwinContext().Get<RoleManager<IdentityRole>>();

                var roles = roleManager.Roles.ToList();
                return roles.Select(r => new RoleDto { RoleName = r.Name, RoleId = r.Id }).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(string userId)
        {
            if (String.IsNullOrEmpty(userId))
                return false;

            var r = await _unitOfWork.UsersRepository.DeleteAsync(userId);

            if (r)
                await _unitOfWork.SaveWorkAsync();

            return r;

        }

        public async Task<bool> DeleteAsync(ApplicationUserDto userDto)
        {
            if (userDto == null)
                return false;

            var user = EntityMapper.Map<ApplicationUserDto, ApplicationUser>(userDto);

            var r = _unitOfWork.UsersRepository.Delete(user);

            if (r)
                await _unitOfWork.SaveWorkAsync();
            return r;
        }


    }
}