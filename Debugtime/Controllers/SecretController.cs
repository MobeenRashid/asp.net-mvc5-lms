using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Debugtime.Controllers
{
    [Authorize]
    public class SecretController : Controller
    {
        // GET: Secret
        public ActionResult Index()
        {
            return Content("You have reached to super secret information");
        }
    }
}