using System.Threading.Tasks;
using System.Web;
using Debugtime.Common.Dtos;
using Debugtime.Common.Model.Input;
using Debugtime.Common.Model.View;
using Debugtime.Common.Wrappers;

namespace Debugtime.Common.Rest_Services.Contracts
{
    public interface IProfileRestService
    {
        Task<string> GetFullNameAsync(string userId);
        Task<string> GetFirstNameAsync(string userId);
        Task<UserAvatarDto> GetAvatarAsync(string userId);

        Task<UserProfileInputModel> GetProfileToEditByIdAsync(string memberId);
        Task<UserProfileViewModel> GetProfileToViewByUserNameAsync(string userName);


        Task<AppHttpResponseMessageWrapper> SaveInfoAsync(UserProfileInputModel profileInfo);
        Task<AppHttpResponseMessageWrapper> SaveAvatarAsync(HttpPostedFileBase avatarInfo);

    }
}