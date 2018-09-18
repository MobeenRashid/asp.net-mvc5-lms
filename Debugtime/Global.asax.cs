using Debugtime.Helpers;
using System;
using System.Runtime.ExceptionServices;

namespace Debugtime
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AppDomain.CurrentDomain.FirstChanceException += FirstChanceExceptionCallBack;
            ConfigurationHelper.ConfigureApp();
        }

        private void FirstChanceExceptionCallBack(object sender, FirstChanceExceptionEventArgs e)
        {
            if(e.Exception is NotImplementedException)
            {
                //do something special when functionality is not implemented
            }
        }
    }
}
