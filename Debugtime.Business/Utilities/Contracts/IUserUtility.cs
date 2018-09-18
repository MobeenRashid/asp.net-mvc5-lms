using System.Collections.Generic;
using System.Threading.Tasks;
using Debugtime.Common.Dtos;
using Debugtime.Common.Wrappers;
using DebugTime.Domain.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Debugtime.Business.Utilities.Contracts
{
    public interface IUserUtility
    {
        Task<ApplicationUserDto> GetUserByIdAsync(string id);
        Task<ApplicationUserDto> GetUserByEmailAsync(string email);
        Task<AppIdentityResultWrapper> AddUserAsync(UserRegisterDto user);
        Task<bool> AddSignInAsync(UserSignInDto user);
        Task<string> GenerateEmailConfirmationTokenAsync(string userId);
        Task<bool> SendEmailAsync(string userId, string emailSubject, string emailMessage);
        Task<IdentityResult> ConfirmEmailAsync(string userId, string token);
        Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistence);
        Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo loginInfo);
        IList<RoleDto> GetRoles();
        Task<bool> DeleteAsync(string userId);
        Task<bool> DeleteAsync(ApplicationUserDto user);
        Task<ApplicationUserDto> GetUserWithProfileAsync(string userId);
        Task<bool> UpdateAsync(ApplicationUserDto userInfo);
    }
}