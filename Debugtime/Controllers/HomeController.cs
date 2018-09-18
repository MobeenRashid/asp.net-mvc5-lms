using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CsQuery.ExtensionMethods;
using Debugtime.Common.Helpers;
using Debugtime.Common.Persistence;
using DebugTime.Domain.Model;
using System.Data.Entity;
using AutoMapper;
using Debugtime.Common.Configurations;
using Debugtime.Common.Model.View;


namespace Debugtime.Controllers
{
    public class HomeController : Controller
    {
        private IMapper _mapper;
        private ApplicationDbContext _context;

        public IMapper Mapper => _mapper ?? (_mapper = new AutoMapperProfileConfiguration().EntityMapper);

        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("terms")]
        public ActionResult Terms()
        {
            return View();
        }

        [Route("aboutus")]
        public ActionResult AboutUs()
        {
            return View();
        }

        [Route("support")]
        public ActionResult Support()
        {
            return View();
        }

        [Route("Result")]
        public ActionResult Result(string searchTerm = "", int page = 1)
        {
            ViewBag.SearchTerm = searchTerm;
            _context = new ApplicationDbContext();
            //Request.QueryString["searchTerm"] = RegexHelper.Replace(searchTerm, @"\s", "+");

            string[] keyWords = RegexHelper.Split(searchTerm, @"\s");

            var result = new List<Course>();

            keyWords.ForEach(k =>
            {
                var items = _context.Courses.Include(c => c.Author.UserProfile).Include(c => c.CourseReviews).Where(c => c.Title.Contains(k) || c.ProgrammingLanguege.Contains(k) || c.Tags.Contains(k) || c.Author.UserProfile.FirstName.Contains(k) || c.Author.UserProfile.LastName.Contains(k))
                    .ToList();
                result.AddRange(items);
            });


            var viewModel = new CourseSearchViewModel(page, 8, 6, result.Count);
            viewModel.Courses = result.Skip(--page * viewModel.PageSize).Take(viewModel.PageSize)
                .Select(item => Mapper.Map<Course, CourseFlexViewModel>(item)).ToList();

            viewModel.SetStart();

            return View(viewModel);
        }
    }
}