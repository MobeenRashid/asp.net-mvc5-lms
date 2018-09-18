using System;
using Debugtime.Business.Utilities.Contracts;
using System.Threading.Tasks;
using Debugtime.Common.Dtos;
using Debugtime.DataAccess.Core.IRepositories;
using Debugtime.DataAccess.Persistence.Repositories;
using DebugTime.Domain.Model;
using Debugtime.Common.Configurations;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;

namespace Debugtime.Business.Utilities.Concretes
{
    public class CourseUtility : AutoMapperProfileConfiguration, ICourseUtility
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourseUtility()
        {
            _unitOfWork = new UnitOfWork();
        }


        public async Task<IList<CourseCatagoryDto>> GetCatagoryAsync()
        {
            try
            {
                var users = await _unitOfWork.CoursesRepository.GetAllCatagoryAsync();
                var mm = users.Select(u => EntityMapper.Map<Catagory, CourseCatagoryDto>(u)).ToList();

                for (var i = 0; i < mm.Count; i++)
                    mm[i].Title = users[i].Title;

                return mm;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public async Task<bool> SaveCourseInfoAsync(CourseDto courseinfo)
        {

            if (courseinfo == null)
                return false;

            var courseEntity = EntityMapper.Map<CourseDto, Course>(courseinfo);
            courseEntity.Level = CourselLevel.Intermediate;
            courseEntity.HasAssessment = false;
            courseEntity.QuizId = "";
            //courseEntity.AuthorId = "a15ecb88-4a83-4c5d-9cc9-7ec94569b264";
            courseEntity.IsDeleted = false;
            courseEntity.UserCount = 0;
            courseEntity.Accessibility = CourseAccessiblity.Premium;
            courseEntity.ProgrammingLanguege = "C# Core";


            _unitOfWork.CoursesRepository.SaveAsync(courseEntity);

            var result = await _unitOfWork.SaveWorkAsync();

            if (result == 0)
                return false;

            return true;
        }

        public async Task<bool> SaveCatagoryAsync(CourseCatagoryDto catagoryInfo)
        {
            
            var catagory = EntityMapper.Map<CourseCatagoryDto, Catagory>(catagoryInfo);
          
            catagory.Id = Guid.NewGuid().ToString();
            _unitOfWork.CoursesRepository.AddCatagory(catagory);

            if (await _unitOfWork.SaveWorkAsync() > 0)
                return true;
            return false;
        }

        public async Task<IList<CourseCatagoryDto>> GetAllCatagoriesAsync()
        {
            var cats = await _unitOfWork.CoursesRepository.GetAllCatagoryAsync();

            var catDtos = cats.Select(u => EntityMapper.Map<Catagory, CourseCatagoryDto>(u)).ToList();

            return (IList<CourseCatagoryDto>)catDtos;
        }

        public async Task<bool> BookMarkAsync(string courseId, string userId)
        {
            var bookMark = new Bookmark
            {
                UserId = userId,
                CourseId = courseId
            };

            try
            {
                _unitOfWork.CoursesRepository.AddBookMark(bookMark);

                var succeeded=(await _unitOfWork.SaveWorkAsync() > 0);
                return succeeded;
            }
            catch (DbUpdateException)
            {
                _unitOfWork.CoursesRepository.RemoveBookMark(bookMark);
                var succeeded = (await _unitOfWork.SaveWorkAsync() > 0);
                return succeeded;
            }
        }

        public async Task<IList<CourseDto>> GetAllCourseAsync()
        {
            var cats = await _unitOfWork.CoursesRepository.GetAllAsync();
            var catDtos = cats.Select(c => EntityMapper.Map<Course, CourseDto>(c)).ToList();
            return (IList<CourseDto>)catDtos;
        }

        public async Task<bool> SaveVideoInfoAsync(CourseVideoDto videoInfo)
        {
            var video = EntityMapper.Map<CourseVideoDto, Video>(videoInfo);

            var coursesection = new CourseSection
            {
                CourseId = videoInfo.CourseSectionId,
                Title = videoInfo.SectionTitle,
                Order = videoInfo.Order,
            };

            _unitOfWork.CoursesRepository.AddCourseSection(coursesection);
            await _unitOfWork.SaveWorkAsync();

            video.Duration = "5:00";
            _unitOfWork.CoursesRepository.AddVideoInfo(video);

            if (await _unitOfWork.SaveWorkAsync() > 0)
                return true;
            return false;
        }

        public async Task<bool> UpdateCourseAsync(CourseDto updateinfo)
        {
            if (updateinfo == null)
                return false;

            var courseEntity = EntityMapper.Map<CourseDto, Course>(updateinfo);
             _unitOfWork.CoursesRepository.updateCourseAsync(courseEntity);

            var result = await _unitOfWork.SaveWorkAsync();

            if (result == 0)
                return false;

            return true;
        }

        public async Task<IList<CourseVideoDto>> GetAllLessonsAsync(string Id)
        {
            if (String.IsNullOrEmpty(Id))
                return null;

            var allLesson = await _unitOfWork.CoursesRepository.GetAllLessonAsync(up => up.CourseSectionId == Id);

            var mm = allLesson.Select(u => EntityMapper.Map<Video, CourseVideoDto>(u)).ToList();

            //for (var i = 0; i < mm.Count; i++)
            //    mm[i]. = allLesson[i].UserAccount.UserName;

            return mm;
        }
    }
}

