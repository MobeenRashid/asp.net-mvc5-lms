using Debugtime.Common.Wrappers;
using Debugtime.Master.Models.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugtime.Master.Rest_Services.Contracts
{
    public interface ICourseRestService
    {
        Task<AppHttpResponseMessageWrapper> SaveInfoAsync(CoursesInputModel courseInfo);

        Task<AppHttpResponseMessageWrapper> updateCourseInfoAsync(CoursesInputModel courseInfo);
        
        Task<AppHttpResponseMessageWrapper> SaveCatagoryAsync(CourseCatagoryModel catagoryInfo);

        Task<AppHttpResponseMessageWrapper> SaveVideoInfoAsync(VideoInputModel videoInfo);

        Task<AppHttpResponseMessageWrapper> GetCatagoryAsync();

        Task<AppHttpResponseMessageWrapper> GetCourseAsync();

        Task<AppHttpResponseMessageWrapper> GetLessonsAsync(string Id);
        
    }
}
