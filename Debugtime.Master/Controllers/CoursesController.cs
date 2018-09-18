using Debugtime.Master.Controllers.Base;
using Debugtime.Master.Models.Input;
using Debugtime.Master;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace Debugtime.Master.Controllers
{
    public class CoursesController : BaseRestController
    {
        // GET: Courses
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Index(string returnUrl)
        {

            ViewBag.ReturnUrl = returnUrl;

            var response = await CourseRestService.GetCourseAsync();

            if (response.HttpResponseMessage.IsSuccessStatusCode)
            {
                var cats = await response.HttpResponseMessage.Content.ReadAsAsync<IList<CoursesInputModel>>();
                return View(cats);

            }
            else
            {

            }
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Edit(string Id = "")
        {
            //ViewBag.ReturnUrl = returnUrl;
            if (Id == "")
            {
                var response = await CourseRestService.GetCatagoryAsync();
                if (response.HttpResponseMessage.IsSuccessStatusCode)
                {
                    var cats = await response.HttpResponseMessage.Content.ReadAsAsync<IList<CourseCatagoryModel>>();

                    ViewBag.Courses = cats.Select(c => new SelectListItem() { Value = c.Id, Text = c.Title });

                    return View();
                }
                else
                {
                    //ViewBag.catagories = new List<SelectListItem>();
                }
            }
            else
            {
                var Catagoryresponse = await CourseRestService.GetCatagoryAsync();
                if (Catagoryresponse.HttpResponseMessage.IsSuccessStatusCode)
                {
                    var catag = await Catagoryresponse.HttpResponseMessage.Content.ReadAsAsync<IList<CourseCatagoryModel>>();
                    ViewBag.Courses = catag.Select(c => new SelectListItem() { Value = c.Id, Text = c.Title });
                }
                var response = await CourseRestService.GetCourseAsync();
                if (response.HttpResponseMessage.IsSuccessStatusCode)
                {
                    var cats = await response.HttpResponseMessage.Content.ReadAsAsync<IList<CoursesInputModel>>();
                    var courseInfo = cats.Where(i => i.Id == Id).FirstOrDefault();
                    return View(courseInfo);
                }
                else
                {
                }
            }


            return View();
        }

        [HttpPost]
        [ValidateInput(true)]
        public async Task<ActionResult> Edit(CoursesInputModel courseInfo, HttpPostedFileBase image)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var response = await CourseRestService.GetCatagoryAsync();

                    if (response.HttpResponseMessage.IsSuccessStatusCode)
                    {
                        ViewBag.Courses = await response.HttpResponseMessage.Content.ReadAsAsync<IList<CourseCatagoryModel>>();
                        return View();
                    }
                    else
                    {

                    }
                }

                if (courseInfo.Id == null)
                {
                    if (image != null)
                    {
                        //attach the uploaded image to the object before saving to Database
                        //Save image to file
                        var filename = image.FileName;
                        var filePathThumbnail = Server.MapPath("/Content/Uploads/Courses");
                        string savedFileName = Path.Combine(filePathThumbnail, filename);
                        image.SaveAs(savedFileName);
                        courseInfo.TitleImagePath = savedFileName;
                    }

                    courseInfo.Id = Guid.NewGuid().ToString();
                    courseInfo.AuthorId = User.Identity.GetUserId(); //"1cefa0ee-6408-4c1d-a705-e6303a73b2e8";
                    var responceMessage = await CourseRestService.SaveInfoAsync(courseInfo);

                    if (responceMessage.HttpResponseMessage.IsSuccessStatusCode)
                        return RedirectToAction("Edit");

                    return RedirectToAction("Failed", "Oops");
                }
                else
                {
                    courseInfo.AuthorId = User.Identity.GetUserId();
                    var responceMessage = await CourseRestService.updateCourseInfoAsync(courseInfo);
                    if (responceMessage.HttpResponseMessage.IsSuccessStatusCode)
                        return RedirectToAction("Edit");

                    return RedirectToAction("Failed", "Oops");

                }
            }
            catch (Exception ex)
            {
                return Content("Exception Occured and Its Handeled :D", ex.Message);
            }


        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> EditLesson(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            var response = await CourseRestService.GetCourseAsync();

            if (response.HttpResponseMessage.IsSuccessStatusCode)
            {
                var cats = await response.HttpResponseMessage.Content.ReadAsAsync<IList<CoursesInputModel>>();
                var model = new VideoInputModel
                {
                    CoursesCatagory = cats.Select(c => new SelectListItem() { Value = c.Id, Text = c.Title }),
                };
                return View(model);

            }
            else
            {

            }
            return View();
        }

        [HttpPost]
        [ValidateInput(true)]
        public async Task<ActionResult> EditLesson(VideoInputModel videoInfo)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    var response = await CourseRestService.GetCourseAsync();

                    if (response.HttpResponseMessage.IsSuccessStatusCode)
                    {
                        var cats = await response.HttpResponseMessage.Content.ReadAsAsync<IList<CoursesInputModel>>();
                        var model = new VideoInputModel
                        {
                            CoursesCatagory = cats.Select(c => new SelectListItem() { Value = c.Id, Text = c.Title }),
                        };
                        return View(model);
                    }
                    else
                    {

                    }
                }
                //videoInfo.Id = Guid.NewGuid().ToString();

                var splits = videoInfo.FileName.Split('\\');

                string videoName = splits[splits.Length - 1];

                string filePath = "~/Static/Courses/Unique/" + videoName;

                videoInfo.Path = filePath;

                var responceMessage = await CourseRestService.SaveVideoInfoAsync(videoInfo);

                if (responceMessage.HttpResponseMessage.IsSuccessStatusCode)
                    return RedirectToAction("EditLesson");

                return RedirectToAction("Failed", "Oops");
            }
            catch (TaskCanceledException ex)
            {
                return Content("Exception Occured and Its Handeled :D", ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Categories(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            //var response = await CourseRestService.GetCatagoryAsync();

            //if (response!=null)
            //{
            //var cats = await response.HttpResponseMessage.Content.ReadAsAsync<IList<CourseCatagoryModel>>();
            //return View(response);
            //ViewBag.catagories = cats.Select(c => new SelectListItem() { Value = c.Id, Text = c.Title });
            //}
            //else
            //{
            //    ViewBag.catagories = new List<SelectListItem>();
            //}

            return View();
        }

        [HttpPost]
        [ValidateInput(true)]
        public async Task<ActionResult> Categories(CourseCatagoryModel catagoryInfo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(catagoryInfo);
                }
                var responceMessage = await CourseRestService.SaveCatagoryAsync(catagoryInfo);

                if (responceMessage.HttpResponseMessage.IsSuccessStatusCode)
                    return RedirectToAction("Categories");

                return RedirectToAction("", "Oops");
            }
            catch (Exception ex)
            {
                return Content("Exception Occured and Its Handeled :D", ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> AllLessons(string Id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(Id);
                }

                var responceMessage = await CourseRestService.GetLessonsAsync(Id);

                if (responceMessage.HttpResponseMessage.IsSuccessStatusCode)
                {
                    var cats = await responceMessage.HttpResponseMessage.Content.ReadAsAsync<IList<VideoInputModel>>();
                    return View(cats);
                }
                else
                {

                }
                return View();
            }
            catch (Exception ex)
            {
                return Content("Exception Occured and Its Handeled :D", ex.Message);
            }
        }

    }
}