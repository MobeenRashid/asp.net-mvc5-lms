using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Debugtime.Common.Dtos;
using Debugtime.Common.Extentions;
using Debugtime.Common.Helpers;
using Debugtime.Common.Model.Input;
using Debugtime.Common.Model.View;
using Debugtime.Common.Rest_Services.Contracts;
using Debugtime.Common.Utilities;
using Debugtime.Common.Wrappers;

namespace Debugtime.Common.Rest_Services.Concretes
{
    public class UserRestService : IUserRestService
    {
        private readonly AppHttpClient _httpClient;
        private AppJsonUtility _appJsonUtility;

        public UserRestService(AppHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public AppJsonUtility AppJsonUtility
        {
            get
            {
                return _appJsonUtility ?? (_appJsonUtility = new AppJsonUtility());
            }
        }

        public async Task<AppHttpResponseMessageWrapper> RegisterMemberAsync(UserRegisterViewModel newUser)
        {
            AppHttpResponseMessageWrapper resultWrapper = null;
            try
            {
                var result = await _httpClient.PostAsJsonAsync("api/account/signup", newUser);

                resultWrapper = new AppHttpResponseMessageWrapper(result);

                if (resultWrapper.HttpResponseMessage.IsSuccessStatusCode)
                {
                    var idAndName = await resultWrapper.HttpResponseMessage.Content.ReadAsAsync<UserIdAndName>();
                    resultWrapper.UserName = idAndName.UserName;
                    resultWrapper.UserId = idAndName.UserId;
                    return resultWrapper;
                }

                if (result.StatusCode == HttpStatusCode.InternalServerError)
                {
                    var exception = await result.Content.ReadAsAsync<Exception>();
                    resultWrapper.ErrorMessage = exception.Message;
                }

                var jsonResult = await result.Content.ReadAsStringAsync();
                resultWrapper.ModelErrors = AppJsonUtility.UnwrapHttpBadRequestResult(jsonResult);
                return resultWrapper;
            }
            catch (Exception exception)
            {
                resultWrapper?.ModelErrors.Add(exception.Message);
                return resultWrapper;
            }

        }

        public async Task<AppHttpResponseMessageWrapper> GetUserNameByEmailAsync(string userEmail)
        {
            var result = await _httpClient.GetAsync($"api/users/{userEmail}/username");

            var wrapperResult = new AppHttpResponseMessageWrapper(result);
            if (result.IsSuccessStatusCode)
            {
                var userName = await result.Content.ReadAsStringAsync();
                wrapperResult.UserName = RegexHelper.CleanHttpResponceString(userName);
            }

            wrapperResult.ErrorMessage = await result.Content.ReadAsStringAsync();
            return wrapperResult;
        }

        public async Task<AppHttpResponseMessageWrapper> SignInMemberAsync(UserSignInDto userInfo)
        {
            var response = await _httpClient.PostAsJsonAsync("api/account/signin/user", userInfo);

            return new AppHttpResponseMessageWrapper(response);
        }

        public async Task<string> GetEmailConfirmationToken(string userId)
        {
            var responceResult = await _httpClient.PostAsync($"api/users/{userId}/email/ConfirmationToken", null);

            if (responceResult.IsSuccessStatusCode)
            {
                string token = await responceResult.Content.ReadAsStringAsync();
                return RegexHelper.CleanHttpResponceString(token);
            }

            return String.Empty;
        }

        public async Task SendEmailAsync(string userId, string subject, string message)
        {
            var emailDto = new EmailDto
            {
                UserId = userId,
                EmailSubject = subject,
                EmailBody = message
            };

            await _httpClient.PostAsJsonAsync("api/users/SendEmail", emailDto);
        }

        public async Task<bool> ConfirmEmailAsync(string userId, string token)
        {
            var responceResult = await _httpClient.PostAsync($"api/users/ConfirmEmail/{userId}/{token}", null);

            if (responceResult.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<IList<UserListViewModel>> GetUsersLisAsync()
        {
            var response = await _httpClient.GetAsync("api/users/list");

            if (!response.IsSuccessStatusCode)
                return null;

            var users = await response.Content.ReadAsAsync<IEnumerable<UserListViewModel>>();

            var userListViewModels = users as IList<UserListViewModel> ?? users.ToList();

            return userListViewModels.Any() ? userListViewModels : null;
        }


        //public async Task<UsageSummaryModel> GetUsageSummaryAsync()
        //{
        //    var response = await _httpClient.GetAsync("api/users/summary");

        //    if (!response.IsSuccessStatusCode)
        //        return null;

        //    var users = await response.Content.ReadAsAsync<UsageSummaryModel>();

        //    var userListViewModels = users as UsageSummaryModel ?? users;

        //    return userListViewModels;
        //}

        public async Task<IList<RoleDto>> GetAllRoles()
        {
            var responce = await _httpClient.GetAsync("api/users/roles");

            if (!responce.IsSuccessStatusCode)
                return null;

            var roles = await responce.Content.ReadAsAsync<IList<RoleDto>>();

            return roles;
        }

        public async Task<UserEditInputModel> GetUserAsync(string userId)
        {
            var responce = await _httpClient.GetAsync($"api/users/{userId}");

            if (!responce.IsSuccessStatusCode)
                return null;

            var user = await responce.Content.ReadAsAsync<UserEditInputModel>();
            return user;
        }

        public async Task<bool> UpdateUserAsync(UserEditInputModel userInfo)
        {
            var responce = await _httpClient.PostAsJsonAsync("api/users/update", userInfo);
            return responce.IsSuccessStatusCode;
        }
    }
}