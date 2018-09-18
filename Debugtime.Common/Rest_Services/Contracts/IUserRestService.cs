using System.Collections.Generic;
using System.Threading.Tasks;
using Debugtime.Common.Dtos;
using Debugtime.Common.Model.Input;
using Debugtime.Common.Model.View;
using Debugtime.Common.Wrappers;

namespace Debugtime.Common.Rest_Services.Contracts
{
    public interface IUserRestService
    {
        Task<AppHttpResponseMessageWrapper> RegisterMemberAsync(UserRegisterViewModel newUser);
        Task<AppHttpResponseMessageWrapper> GetUserNameByEmailAsync(string viewModelEmail);
        Task<AppHttpResponseMessageWrapper> SignInMemberAsync(UserSignInDto userInfo);
        Task<string> GetEmailConfirmationToken(string userId);
        Task SendEmailAsync(string userId, string subject, string message);
        Task<bool> ConfirmEmailAsync(string userId, string token);
        Task<IList<UserListViewModel>> GetUsersLisAsync();
        Task<IList<RoleDto>> GetAllRoles();
        Task<UserEditInputModel> GetUserAsync(string userId);
        Task<bool> UpdateUserAsync(UserEditInputModel userInfo);
        //Task<UsageSummaryModel> GetUsageSummaryAsync();
    }
}