using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Debugtime.Common.Dtos;

namespace Debugtime.Business.Utilities.Contracts
{
    public interface IProfileUtility
    {
        Task<UserAvatarDto> GetAvatarByIdAsync(string id);
        Task<string> GetFullNameByIdAsync(string id);
        Task<string> GetFirstNameByIdAsync(string id);

        Task<UserProfileInputDto> GetProfileToEditByIdAsync(string id);
        Task<UserProfileViewDto> GetProfileToViewByUserNameAsync(string id);

        Task<bool> UpdateProfileAsync(UserProfileInputDto info);
        Task<bool> UploadAvatarInfoAsync(UserAvatarDto info);
        Task<IList<UserProfileListDto>> GetProfileListAsync();

        //Task<UsageSymmaryDto> GetUsageSummaryAsync();

    }
}
