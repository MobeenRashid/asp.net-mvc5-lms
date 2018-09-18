using Debugtime.Common.Dtos;
using DebugTime.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Debugtime.DataAccess.Core.IRepositories
{
    public interface ICoursesRepository
    {
        Course SaveAsync(Course courseInfo);

        Task<IList<Catagory>> GetAllCatagoryAsync();

        Task<IList<Course>> GetAllAsync();

        Task<IList<Course>> GetAllAsync<T>(Expression<Func<Course, T>> includeT) where T : class;

        Task<IList<Course>> GetAllAsync<T, T2>(Expression<Func<Course, T>> includeT, Expression<Func<Course, T2>> includeT2) where T : class;

        Task<IList<Course>> GetAllAsync<T, T2, T3>(Expression<Func<Course, T>> includeT, Expression<Func<Course, T2>> includeT2, Expression<Func<Course, T3>> includeT3) where T : class;



        Task<IList<Video>> GetAllLessonAsync(Expression<Func<Video, bool>> predicate);

        Task<IList<Video>> GetAllLessonAsync<T>(Expression<Func<Video, bool>> predicate, Expression<Func<Video, T>> includeT) where T : class;

        Task<IList<Video>> GetAllLessonAsync<T, T1>(Expression<Func<Video, bool>> predicate, Expression<Func<Video, T>> includeT1, Expression<Func<Video, T1>> includeT2) where T : class;




        Catagory AddCatagory(Catagory catagory); 
        Video AddVideoInfo(Video video);
        void AddCourseSection( CourseSection coursesection);
        void AddBookMark(Bookmark bookMark);
        void RemoveBookMark(Bookmark bookMark);
        void updateCourseAsync(Course courseInfo);
        
    }
}
