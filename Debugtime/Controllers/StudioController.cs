using System;
using System.Linq;
using System.Web.Mvc;
using Debugtime.Common.Persistence;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using CsQuery.ExtensionMethods;
using Debugtime.Common.Configurations;
using Debugtime.Common.Infrastructure;
using Debugtime.Common.Model.View;
using Debugtime.Common.Static;
using DebugTime.Domain.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Debugtime.Controllers
{
    [Authorize]
    [RoutePrefix("studio")]
    public class StudioController : Controller
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;
        private AppUserManager _userManager;

        public AppUserManager UserManager => _userManager ?? (_userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>());

        public IMapper Mapper => _mapper ?? (_mapper = new AutoMapperProfileConfiguration().EntityMapper);



        [Route("play/{courseId}/section/{section}/lesson/{lesson}")]
        public async Task<ActionResult> Play(string courseId, int section = 1, int lesson = 1)
        {
            _context = new ApplicationDbContext();

            if (!_context.Courses.Any(c => c.Id == courseId))
                return RedirectToAction("Error", "Oops",
                    new
                    {
                        StatusCode = "404",
                        Subject = "Course not found",
                        Message = "The lesson you are trying to access is not availaible.",
                        HelpUrl = Url.Action("Index", "Courses", new { area = "Library" }),
                        HelpText = "Go to Library"
                    });

            if (!await UserManager.HasCourseAsync(courseId, User.Identity.GetUserId()))
                return RedirectToAction("ConfirmPurchase", "Purchase", new { area = "Cart", cId = courseId });

            var courseToPlay = _context.Courses.Include(c => c.CourseSections.Select(cs => cs.Videos)).SingleOrDefault(c => c.Id == courseId);

            if (courseToPlay == null)
                return RedirectToAction("Error", "Oops",
                    new
                    {
                        StatusCode = "404",
                        Subject = "Content not found",
                        Message = "The lesson you are trying to access is not availaible.",
                        HelpUrl = Url.Action("Detail", "Courses", new { cId = courseId })
                    });
            var userId = User.Identity.GetUserId();
            if (!_context.UsersCoursesProgresses.Any(q => q.CourseId == courseToPlay.Id && q.UserId == userId))
            {
                _context.UsersCoursesProgresses.Where(q => q.UserId == userId).ForEach(q => q.IsRecent = false);

                var courseProgress = new UserCourseProgress
                {
                    CourseId = courseId,
                    UserId = userId,
                    IsRecent = true
                };

                _context.UsersCoursesProgresses.Add(courseProgress);
                _context.SaveChanges();
            }

            var viewModel = Mapper.Map<Course, CoursePlayViewModel>(courseToPlay);

            var sectionToPlay = _context.CourseSections.SingleOrDefault(c => c.CourseId == courseToPlay.Id && c.Order == section);
            var sectionCount = _context.CourseSections.Count(c => c.CourseId == courseToPlay.Id);

            if (sectionToPlay != null)
            {
                var videoToPlay = _context.Videos.FirstOrDefault(
                    v => v.CourseSectionId == sectionToPlay.CourseId && v.SectionTitle == sectionToPlay.Title && v.Order == lesson);

                var lessonCount = _context.Videos.Count(
                    v => v.CourseSectionId == sectionToPlay.CourseId && v.SectionTitle == sectionToPlay.Title);

                if (videoToPlay != null)
                {
                    viewModel.PlayerModel.Src = String.Concat(ResourceStrings.ApiBaseAddress,
                        $"/api/stream/{sectionToPlay.CourseId}/{sectionToPlay.Title}/{videoToPlay.Order}");

                    viewModel.LessonTitle = videoToPlay.Title;
                    viewModel.PlayerModel.AutoPlay = true;
                    viewModel.SetNextLesson(section, lesson);
                    viewModel.SetPrevLesson(section, lesson);

                    var courseProgress =
                        _context.UsersCoursesProgresses.SingleOrDefault(
                            q => q.CourseId == courseToPlay.Id && q.UserId == userId);

                    var sectionShare = (decimal)100 / sectionCount;
                    var lessionShare = sectionShare / lessonCount;

                    if (courseProgress != null)
                    {
                        courseProgress.Level = courseProgress.Level < section ? section : courseProgress.Level;
                        courseProgress.Lesson = lesson;
                        courseProgress.Progress = (int)Math.Round(--section * sectionShare + lesson * lessionShare);
                        _context.Entry(courseProgress).State = EntityState.Modified;
                        _context.SaveChanges();
                    }

                    return View(viewModel);
                }
            }

            return RedirectToAction("Error", "Oops",
                new
                {
                    StatusCode = "404",
                    Subject = "Content not found",
                    Message = "The lesson you are trying to access is not availaible.",
                    HelpUrl = Url.Action("Detail", "Courses", new { cId = courseId })
                });
        }
    }
}
