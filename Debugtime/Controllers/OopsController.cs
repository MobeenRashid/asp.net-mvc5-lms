using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Debugtime.Controllers
{
    [RoutePrefix("Oops")]
    public class OopsController : Controller
    {
        


        [Route("Error")]
        public ActionResult Error(string statusCode="400",string subject = @"Bad Request", string message = @"We recieved a bad request, it might be a varification error.", string helpUrl = "/", string urlText = @"Try Again")
        {
            ViewBag.StatusCode = statusCode;
            ViewBag.Subject = subject;
            ViewBag.Message = message;
            ViewBag.HelpUrl = helpUrl;
            ViewBag.HelpText = urlText;

            return View();
        }
        

    }
}