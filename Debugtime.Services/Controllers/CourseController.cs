using System;
using System.Linq;
using Debugtime.Common.Dtos;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Debugtime.Services.Controllers.Base;
using System.Data.SqlClient;
using System.Web;
using System.Net.Http;
using System.IO;
using System.Collections.Generic;


namespace Debugtime.Services.Controllers
{
    [RoutePrefix("api/course")]
    public class CourseController : BaseApiController
    {
        public CourseController()
        {

        }

        [Route("AddNew")]
        [HttpPost]
        public async Task<IHttpActionResult> AddNew(CourseDto courseInfo)
        {
            try
            {
                var result = await UnitOfUtility.CourseUtility.SaveCourseInfoAsync(courseInfo);
                if (result)
                    return Ok();

                return StatusCode(HttpStatusCode.BadGateway);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpPost]
        [Route("AddCatagory")]
        public async Task<IHttpActionResult> AddCatagory(CourseCatagoryDto catagoryInfo)
        {
            try
            {
                var result = await UnitOfUtility.CourseUtility.SaveCatagoryAsync(catagoryInfo);
                if (result)
                    return Ok();

                return StatusCode(HttpStatusCode.BadGateway);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

        }


        [HttpPost]
        [Route("AddVideoName")]
        public async Task<IHttpActionResult> AddVideoName(CourseVideoDto videoInfo)
        {

            //string sourcePath = videoInfo.Path;
            //string targetPath = HttpContext.Current.Server.MapPath("~/Static/Courses/"+videoInfo.CourseSectionId+"/ "+videoInfo.SectionTitle+"/");

            //if (!Directory.Exists(targetPath))
            //{
            //    Directory.CreateDirectory(targetPath);
            //}
            //File.Copy(sourcePath, targetPath, true);

            //videoInfo.Path = targetPath;
            try
            {
                var result = await UnitOfUtility.CourseUtility.SaveVideoInfoAsync(videoInfo);
                if (result)
                    return Ok(result);

                return NotFound();
            }
            catch (Exception)
            {
                return InternalServerError();
            }

        }


        [HttpPost]
        [Route("AddVideoInfo")]
        public IHttpActionResult AddVideoInfo()
        {

            //var hello = HttpContext.Current.Request.Params["file"];

            var httpRequest = HttpContext.Current;

            foreach (string file in httpRequest.Request.Files)
            {
                var FileDataContent = httpRequest.Request.Files[file];
                if (FileDataContent != null && FileDataContent.ContentLength > 0)
                {
                    // take the input stream, and save it to a temp folder using
                    // the original file.part name posted
                    var stream = FileDataContent.InputStream;
                    var fileName = Path.GetFileName(FileDataContent.FileName);
                    var UploadPath = HttpContext.Current.Server.MapPath("~/Static/Courses/Unique");
                    Directory.CreateDirectory(UploadPath);
                    string path = Path.Combine(UploadPath, fileName);
                    try
                    {
                        if (System.IO.File.Exists(path))
                            System.IO.File.Delete(path);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }
                        MergeVideoChunksFiles merge = new MergeVideoChunksFiles();
                        merge.MergeFile(path);

                        //CourseVideoDto videoInfo = new CourseVideoDto();
                        //videoInfo.Path = path;
                        //var result = UnitOfUtility.CourseUtility.SaveVideoInfoAsync(videoInfo);
                        //if (result)
                        //return Ok(result);

                        //return StatusCode(HttpStatusCode.BadGateway);

                    }
                    catch (IOException ex)
                    {
                        // handle
                    }
                }
            }
            return Ok();


            //return new HttpResponseMessage()
            //{
            //    StatusCode = System.Net.HttpStatusCode.OK,
            //    Content = new StringContent("File uploaded.")
            //};


        }

        [Route("{Id}/getlessons")]
        [HttpGet]
        public async Task<IHttpActionResult> GetLessons(string Id)
        {
            try
            {
                var result = await UnitOfUtility.CourseUtility.GetAllLessonsAsync(Id);

                if (result != null)
                    return Ok(result);

                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }



        [Route("GetAllCatagory")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllCatagory()
        {
            try
            {
                var result = await UnitOfUtility.CourseUtility.GetAllCatagoriesAsync();

                if (result != null)
                    return Ok(result);

                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }

        }

        [Route("GetAllCourse")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllCourse()
        {
            try
            {
                var result = await UnitOfUtility.CourseUtility.GetAllCourseAsync();

                if (result != null && result.Any())
                    return Ok(result);

                return StatusCode(HttpStatusCode.BadGateway);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

        }

        [HttpPost]
        [Route("{courseId}/bookmark/{userId}")]
        public async Task<IHttpActionResult> AddBookMark(string courseId, string userId)
        {
            try
            {
                bool succeeded = await UnitOfUtility.CourseUtility.BookMarkAsync(courseId, userId);
                if (succeeded)
                    return Ok<object>(null);

                return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPut]
        [Route("updateCourseInfo")]
        public async Task<IHttpActionResult> PutUpdateCourseInfo(CourseDto catagoryInfo)
        {
            try
            {
                var result = await UnitOfUtility.CourseUtility.UpdateCourseAsync(catagoryInfo);
                if (result)
                    return Ok();

                return StatusCode(HttpStatusCode.BadGateway);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

        }

    }
}