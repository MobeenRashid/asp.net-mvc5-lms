using Debugtime.Master.Models.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;


namespace Debugtime.Master.Controllers
{
    public class PostsController : Controller
    {
        // GET: Posts
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            CourseCatagoryModel model = new CourseCatagoryModel();
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Add(CourseCatagoryModel model)
        {
            return View(model);
        }

        public ActionResult Categories()
        {
            return View();
        }
    }

}