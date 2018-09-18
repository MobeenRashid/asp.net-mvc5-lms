using Debugtime.DataAccess.Core.IRepositories;
using Debugtime.Common.Configurations;
using Debugtime.Common.Persistence;
using DebugTime.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;

namespace Debugtime.DataAccess.Persistence.Repositories
{
    public class CoursesRepository : AutoMapperProfileConfiguration, ICoursesRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CoursesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<Course>> GetAllAsync()
        {
            return await _dbContext.Courses.ToListAsync();
        }

        public async Task<IList<Course>> GetAllAsync<T>(Expression<Func<Course, T>> includeT) where T : class
        {
            return await _dbContext.Courses.Include(includeT).ToListAsync();
        }

        public async Task<IList<Course>> GetAllAsync<T, T2>(Expression<Func<Course, T>> includeT, Expression<Func<Course, T2>> includeT2) where T : class
        {
            return await _dbContext.Courses.Include(includeT).Include(includeT2).ToListAsync();
        }

        public async Task<IList<Course>> GetAllAsync<T, T2, T3>(Expression<Func<Course, T>> includeT, Expression<Func<Course, T2>> includeT2, Expression<Func<Course, T3>> includeT3) where T : class
        {
            return await _dbContext.Courses.Include(includeT).Include(includeT2).Include(includeT3).ToListAsync();
        }

        public Catagory AddCatagory(Catagory catagory)
        {
            return _dbContext.Catagory.Add(catagory);
        }

        public async Task<IList<Catagory>> GetAllCatagoryAsync()
        {
            return await _dbContext.Catagory.ToListAsync();
        }

        public Course SaveAsync(Course courseInfo)
        {
            return _dbContext.Courses.Add(courseInfo);
        }

        public void AddBookMark(Bookmark bookMark)
        {
            _dbContext.UserBookmarks.Add(bookMark);
        }

        public void RemoveBookMark(Bookmark bookMark)
        {
            _dbContext.Entry(bookMark).State = EntityState.Deleted;
        }

        public Video AddVideoInfo(Video video)
        {
            return _dbContext.Videos.Add(video);
        }

        public void AddCourseSection(CourseSection coursesection)
        {
            _dbContext.CourseSections.Add(coursesection);
        }

        public  void updateCourseAsync(Course courseInfo)
        {
          _dbContext.Entry(courseInfo).State = EntityState.Modified;
            
        }

        public async Task<IList<Video>> GetAllLessonAsync(Expression<Func<Video, bool>> predicate)
        {
            return await _dbContext.Videos.Where(u => u.IsDeleted == false).ToListAsync();
        }

        public async Task<IList<Video>> GetAllLessonAsync<T>(Expression<Func<Video, bool>> predicate, Expression<Func<Video, T>> includeT) where T : class
        {
            return await _dbContext.Videos.Include(includeT).Where(u => u.IsDeleted == false).ToListAsync();

        }

        public async Task<IList<Video>> GetAllLessonAsync<T, T1>(Expression<Func<Video, bool>> predicate, Expression<Func<Video, T>> includeT1, Expression<Func<Video, T1>> includeT2) where T : class
        {
            return await _dbContext.Videos.Include(includeT1).Include(includeT2).Where(u => u.IsDeleted == false).ToListAsync();
        }
    }
}
