using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using CsQuery.ExtensionMethods;
using Debugtime.Common.Persistence;
using Debugtime.Common.Configurations;
using Debugtime.Common.Helpers;
using Debugtime.Common.Model.View;
using DebugTime.Domain.Model;
using Microsoft.AspNet.Identity;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Debugtime.Areas.Library.Model.Input;
using Debugtime.Common.Static;

namespace Debugtime.Areas.Library.Controllers
{
    [RouteArea("Library")]
    public class CoursesController : Controller
    {
        private IMapper _mapper;

        public IMapper Mapper =>
            _mapper ?? (_mapper = new AutoMapperProfileConfiguration().EntityMapper);

        private ApplicationDbContext _context;

        [Route("")]
        public ActionResult Index(int take = 0)
        {

            _context = new ApplicationDbContext();

            var pageSize = 8;

            var courses = _context.Courses.Include(c => c.Author.UserProfile)
                .Include(c => c.CourseReviews).Include(c => c.Bookmarks).Take(pageSize * ++take).ToList();

            var viewModel = courses.Select(c => Mapper.Map<Course, CourseCardViewModel>(c)).ToList();

            var model = new CourseListViewModel
            {
                StickerViewModel = viewModel,
                Take = take
            };

            if (pageSize * take >= _context.Courses.Count())
                model.HaveMore = false;

            for (var i = 0; i < model.StickerViewModel.Count; i++)
                model.StickerViewModel[i].AuthorName = courses[i].Author.UserProfile.FullName;

            return View(model);
        }


        [Route("Latest")]
        public ActionResult Latest()
        {

            _context = new ApplicationDbContext();

            var pageSize = 8;

            var courses = _context.Courses.Include(c => c.Author.UserProfile)
                .Include(c => c.CourseReviews).Include(c => c.Bookmarks).OrderByDescending(c => c.DateCreated).Take(pageSize).ToList();

            var viewModel = courses.Select(c => Mapper.Map<Course, CourseCardViewModel>(c)).ToList();

            var model = new CourseListViewModel
            {
                StickerViewModel = viewModel,
            };


            for (var i = 0; i < model.StickerViewModel.Count; i++)
                model.StickerViewModel[i].AuthorName = courses[i].Author.UserProfile.FullName;

            return View(model);
        }

        [Route("Featured")]
        public ActionResult Featured()
        {

            _context = new ApplicationDbContext();

            var pageSize = 8;

            var courses = _context.Courses.Include(c => c.Author.UserProfile)
                .Include(c => c.CourseReviews).Include(c => c.Bookmarks).OrderByDescending(c => new { c.DateCreated,c.UserCount}).Take(pageSize).ToList();

            var viewModel = courses.Select(c => Mapper.Map<Course, CourseCardViewModel>(c)).ToList();

            var model = new CourseListViewModel
            {
                StickerViewModel = viewModel
            };


            for (var i = 0; i < model.StickerViewModel.Count; i++)
                model.StickerViewModel[i].AuthorName = courses[i].Author.UserProfile.FullName;

            return View(model);
        }


        [Route("Course/{CourseId}")]
        public ActionResult Detail(string courseId)
        {
            _context = new ApplicationDbContext();

            var course = _context.Courses.Include(c => c.Author.UserProfile)
                .Include(c => c.CourseReviews.Select(u => u.User.UserProfile)).Include(c => c.CourseSections.Select(v => v.Videos)).SingleOrDefault(c => c.Id == courseId);

            var viewModel = Mapper.Map<Course, CourseDetailViewModel>(course);
            viewModel.AuthorModel = Mapper.Map<UserProfile, AuthorViewModel>(course?.Author.UserProfile);

            int count = 0;
            viewModel.CourseReviews.ForEach(cr =>
            {
                cr.User = Mapper.Map<UserProfile, UserReviewModel>(course?.CourseReviews.ElementAt(count).User
                    .UserProfile);
                count++;
            });

            viewModel.AuthorModel.UserName = course?.Author.UserName;
            viewModel.PlayerModel.Src = String.Concat(ResourceStrings.ApiBaseAddress, $"/api/stream/{viewModel.Id}/{viewModel.CourseSections[0].Title}/{viewModel.CourseSections[0].Videos[0].Order}");

         


            return View(viewModel);
        }

   
        [Route("course/{courseid}/thumbnail")]
        public ActionResult GetThumbnail(string courseId)
        {
            _context = new ApplicationDbContext();

            var course = _context.Courses.FirstOrDefault(c => c.Id == courseId);

            byte[] fBytes;

            if (!String.IsNullOrEmpty(course?.ThumbnailPath))
            {
                var fPath = Server.MapPath(course.ThumbnailPath);
                fBytes = System.IO.File.ReadAllBytes(fPath);
                return File(fBytes, "image/png");
            }

            var n = new Random().Next(1, 5);
            var path = $"~/Content/Images/Courses/Thumbnail-{n}.png";

            fBytes = System.IO.File.ReadAllBytes(Server.MapPath(path));
            return File(fBytes, "image/png");
        }

        [HttpPost]
        [Route("course/{courseId}/bookmark")]
        [Authorize]
        public bool AddBookmark(string courseId)
        {
            _context = new ApplicationDbContext();
            var bookMark = new Bookmark
            {
                CourseId = courseId,
                UserId = User.Identity.GetUserId()
            };

            try
            {
                _context.UserBookmarks.Add(bookMark);
                if (_context.SaveChanges() > 0)
                    return true;
                return false;
            }
            catch (SqlException)
            {
                _context.UserBookmarks.Remove(bookMark);
                if (_context.SaveChanges() > 0)
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpPost]
        [Authorize]
        [Route("AddReview")]
        public ActionResult AddReview(CourseReviewInputModel model)
        {
            try
            {
                _context = new ApplicationDbContext();
                var review = new CourseReview
                {
                    UserId = User.Identity.GetUserId(),
                    CourseId = model.CourseId,
                    Review = model.UserReview,
                    Stars = model.UserStars
                };

                _context.CourseReviews.Add(review);
                _context.SaveChanges();

                return RedirectToAction("Detail", "Courses", new { area = "Library", courseId = model.CourseId });

            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Oops", new { StatusCode = "500", Subject = "Error Ocurred", Message = "Submit view failed due to internal error, please try again", HelpUrl = Url.Action("Detail", "Courses", new { area = "Library", courseId = model.CourseId }) });
            }

        }

        [Authorize]
        [Route("Mine")]
        public ActionResult Mine()
        {
            _context = new ApplicationDbContext();
            var userId = User.Identity.GetUserId();

            var myCourses = _context.UserCourses.Include(c => c.Course.Author.UserProfile).Include(c=>c.Course.CourseReviews).Where(c => c.UserId == userId).ToList();


            List<CourseCardViewModel> courseStickers=new List<CourseCardViewModel>();

            foreach (var item in myCourses)
            {
                var cs = Mapper.Map<Course, CourseCardViewModel>(item.Course);
                courseStickers.Add(cs);
            }

            for (var i = 0; i < courseStickers.Count; i++)
                courseStickers[i].AuthorName = myCourses[i].Course.Author.UserProfile.FullName;

            var viewModel = new CourseListViewModel
            {
                StickerViewModel = courseStickers
            };

            return View(viewModel);
        }
    }
}