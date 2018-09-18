using Debugtime.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugtime.Business.Utilities.Contracts
{
    public interface ICourseUtility
    {
        Task<bool> SaveCourseInfoAsync(CourseDto info);
        Task<bool> SaveCatagoryAsync(CourseCatagoryDto catagoryInfo);
        Task<bool> SaveVideoInfoAsync(CourseVideoDto videoInfo);

        Task<IList<CourseCatagoryDto>> GetAllCatagoriesAsync();
        Task<IList<CourseDto>> GetAllCourseAsync();
        Task<bool> BookMarkAsync(string courseId, string userId);

        Task<bool> UpdateCourseAsync(CourseDto updateinfo);


        Task<IList<CourseVideoDto>> GetAllLessonsAsync(string Id);

    }
}
