using System.Linq;
using System.Web.Mvc;
using Debugtime.Common.Persistence;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using AutoMapper;
using Debugtime.Common.Configurations;
using Debugtime.Common.Model.View;
using DebugTime.Domain.Model;

namespace Debugtime.Areas.Dashboard.Controllers
{
    [Authorize]
    [RouteArea("dashboard")]
    public class DashboardController : Controller
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = new AutoMapperProfileConfiguration().EntityMapper);

        [Route("")]
        public ActionResult Index(string username)
        {
            _context = new ApplicationDbContext();
            var userId = User.Identity.GetUserId();

            var recentCourses = _context.UserCourses.Include(uc=>uc.Course.UserCourseProgresses).Where(u => u.UserId == userId).OrderByDescending(u => u.Date).Take(6).ToList();

            var inProgress =
                recentCourses.SingleOrDefault(c => c.Course.UserCourseProgresses.Any(cr => cr.IsRecent));

            var bookMarks = _context.Courses.Include(c=>c.UserCourseProgresses).Include(c=>c.Author.UserProfile).Include(c=>c.CourseReviews).Include(c=>c.Bookmarks).Where(q => q.Bookmarks.Any(b => b.UserId == userId)).ToList();

            var viewModel = new DashboardViewModel
            {
                InProgress = Mapper.Map<Course, CourseDashboardViewModel>(inProgress?.Course),
                RecentlyPlayed = recentCourses.Select(c=>Mapper.Map<Course,CourseDashboardViewModel>(c.Course)).ToList(),
                Bookmarks = bookMarks.Select(c => Mapper.Map<Course, CourseCardViewModel>(c))
            };

            for (int i = 0; i < bookMarks.Count; i++)
                viewModel.Bookmarks.ElementAt(i).AuthorName = bookMarks[i].Author.UserProfile.FullName;

            return View(viewModel);
        }
    }
}