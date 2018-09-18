using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Debugtime.Common.Dtos;
using Debugtime.Common.Extentions;
using Debugtime.Common.Helpers;
using Debugtime.Common.Model.Input;
using Debugtime.Common.Model.View;
using Debugtime.Common.Rest_Services.Contracts;
using Debugtime.Common.Wrappers;
using Microsoft.AspNet.Identity;

namespace Debugtime.Common.Rest_Services.Concretes
{
    public class ProfileRestService : IProfileRestService
    {
        private readonly AppHttpClient _httpClient;
        public ProfileRestService(AppHttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<UserProfileInputModel> GetProfileToEditByIdAsync(string memberId)
        {
            var result = await _httpClient.GetAsync($"api/profile/{memberId}/edit");

            if (result.IsSuccessStatusCode)
            {
                var userInputmodel = await result.Content.ReadAsAsync<UserProfileInputModel>();
                return userInputmodel;
            }
            return null;
        }

        public async Task<UserProfileViewModel> GetProfileToViewByUserNameAsync(string userName)
        {
            var result = await _httpClient.GetAsync($"api/profile/{userName}/view");

            if (result.IsSuccessStatusCode)
            {
                var viewmodel = await result.Content.ReadAsAsync<UserProfileViewModel>();
                return viewmodel;
            }
            return null;
        }

        public async Task<AppHttpResponseMessageWrapper> SaveAvatarAsync(HttpPostedFileBase avatarInfo)
        {

            var modelInfo = new UploadAvatarInputModel
            {
                ProfileId = HttpContext.Current.User.Identity.GetUserId(),
                ContentType = avatarInfo.ContentType,
                FileName = avatarInfo.FileName
            };

            using (BinaryReader reader = new BinaryReader(avatarInfo.InputStream))
            {
                modelInfo.Content = reader.ReadBytes((int)avatarInfo.InputStream.Length);
            }


            var responce = await _httpClient.PostAsJsonAsync("api/profile/avatar/upload", modelInfo);

            var responceWrapper = new AppHttpResponseMessageWrapper(responce);

            if (!responce.IsSuccessStatusCode)
                responceWrapper.ErrorMessage = await responce.Content.ReadAsStringAsync();

            return new AppHttpResponseMessageWrapper(responce);
        }

        public async Task<AppHttpResponseMessageWrapper> SaveInfoAsync(UserProfileInputModel profileInfo)
        {
            var resonce = await _httpClient.PostAsJsonAsync("api/profile/save", profileInfo);
            return new AppHttpResponseMessageWrapper(resonce);
        }

        public async Task<string> GetFullNameAsync(string userId)
        {
            if (!String.IsNullOrEmpty(userId))
            {
                var result = await _httpClient.GetAsync($"api/profile/{userId}/fullName");
                if (result.IsSuccessStatusCode)
                {
                    var userName = await result.Content.ReadAsStringAsync();
                    return RegexHelper.CleanHttpResponceString(userName);
                }
            }
            return "Guest User";
        }

        public async Task<string> GetFirstNameAsync(string userId)
        {
            var result = await _httpClient.GetAsync($"api/profile/{userId}/firstname");
           

            if (result.IsSuccessStatusCode)
            {
                var userName = await result.Content.ReadAsStringAsync();
                return RegexHelper.CleanHttpResponceString(userName);
            }

            return "Guest User";
        }

        public async Task<UserAvatarDto> GetAvatarAsync(string userId)
        {
            var result = await _httpClient.GetAsync($"api/profile/{userId}/avatar");

            if (result.IsSuccessStatusCode)
            {
                var avatarInfo = await result.Content.ReadAsAsync<UserAvatarDto>();
                return avatarInfo;
            }
            return null;
        }
    }
}