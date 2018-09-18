using System.Web.Mvc;

namespace Debugtime.Areas.Library
{
    public class LibraryAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "Library";

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Library_default",
                "Library/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}