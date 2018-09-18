using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Debugtime.App_Start
{
    public class FilterConfig
    {
        public static void Register(GlobalFilterCollection filters)
        {
            filters.Add(new RequireHttpsAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}