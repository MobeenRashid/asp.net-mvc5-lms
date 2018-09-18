using Debugtime.Master.Rest_Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Debugtime.Common.Wrappers;
using Debugtime.Master.Models.Input;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using Debugtime.Master.Extensions;

namespace Debugtime.Master.Rest_Services.Concretes
{
    public class CourseRestService : ICourseRestService
    {
        private readonly AppHttpClientMaster _httpClient;

        public CourseRestService(AppHttpClientMaster httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AppHttpResponseMessageWrapper> GetCatagoryAsync()
        {
            var responce = await _httpClient.GetAsync("api/course/getallcatagory");

            return new AppHttpResponseMessageWrapper(responce);
        }

        public async Task<AppHttpResponseMessageWrapper> GetCourseAsync()
        {
            var responce = await _httpClient.GetAsync("api/course/getallcourse");
            return new AppHttpResponseMessageWrapper(responce);
        }

        public async Task<AppHttpResponseMessageWrapper> GetLessonsAsync(string Id)
        {
            var responce = await _httpClient.GetAsync("api/course/{Id}/getlessons");
            return new AppHttpResponseMessageWrapper(responce);
        }

        public async Task<AppHttpResponseMessageWrapper> SaveCatagoryAsync(CourseCatagoryModel catagory)
        {
            var responce = await _httpClient.PostAsJsonAsync("api/course/addcatagory", catagory);
            return new AppHttpResponseMessageWrapper(responce);
        }

        public async Task<AppHttpResponseMessageWrapper> SaveInfoAsync(CoursesInputModel course)
        {
            var responce = await _httpClient.PostAsJsonAsync("api/course/addnew", course);
            return new AppHttpResponseMessageWrapper(responce);
        }

        public async Task<AppHttpResponseMessageWrapper> SaveVideoInfoAsync(VideoInputModel videoInfo)
        {
            var responce = await _httpClient.PostAsJsonAsync("api/course/AddVideoName", videoInfo);
            return new AppHttpResponseMessageWrapper(responce);
        }


        public async Task<AppHttpResponseMessageWrapper> updateCourseInfoAsync(CoursesInputModel updateInfo)
        {
            var responce = await _httpClient.PutAsJsonAsync("api/course/updateCourseInfo", updateInfo);
            return new AppHttpResponseMessageWrapper(responce);
        }
    }
}