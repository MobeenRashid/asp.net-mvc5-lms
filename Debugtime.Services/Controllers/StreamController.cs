using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Http;
using Debugtime.Common.Persistence;

namespace Debugtime.Services.Controllers
{
    [RoutePrefix("api/stream")]
    public class StreamController : ApiController
    {
        private ApplicationDbContext _context;
        private string _lessonPath;

        [HttpGet]
        [Route("{SectionId}/{SectionTitle}/{Lesson}")]
        public HttpResponseMessage Play(string sectionId, string sectionTitle, int lesson)
        {
            _context = new ApplicationDbContext();

            var lessonToPlay = _context.Videos.FirstOrDefault(
                vid => vid.CourseSectionId == sectionId && vid.SectionTitle == sectionTitle && vid.Order == lesson);
            var responce = Request.CreateResponse();

            if (String.IsNullOrEmpty(lessonToPlay?.Path))
            {
                responce.StatusCode = HttpStatusCode.NotFound;
                return responce;
            }

            _lessonPath = HttpContext.Current.Server.MapPath(lessonToPlay.Path);

            var streamContent = new PushStreamContent((Action<Stream, HttpContent, TransportContext>)OnStreamAvailable);
            responce.Content = streamContent;
            return responce;
        }

        private async void OnStreamAvailable(Stream stream, HttpContent httpContent, TransportContext arg3)
        {
            var bufferSize = 1000;
            var buffer = new byte[bufferSize];

            using (var fileStream = File.Open(_lessonPath, FileMode.Open, FileAccess.Read))
            {
                var totalSize = (int)fileStream.Length;

                while (totalSize > 0)
                {
                    var count = totalSize > bufferSize ? bufferSize : totalSize;

                    var chunkSize = await fileStream.ReadAsync(buffer, 0, count);
                    
                    await stream.WriteAsync(buffer,0,chunkSize);
                    totalSize -= chunkSize;
                }
            }

        }
    }
}