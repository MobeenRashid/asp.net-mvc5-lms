﻿using System.Web.Mvc;

namespace Debugtime.Areas.Quiz
{
    public class QuizAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "Quiz";

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Quiz_default",
                "Quiz/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}