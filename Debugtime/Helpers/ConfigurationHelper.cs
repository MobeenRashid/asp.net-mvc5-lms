using Debugtime.Common.Configurations;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace Debugtime.Helpers
{
    public class ConfigurationHelper
    {
        public static void ConfigureApp()
        {
            RouteTable.Routes.MapMvcAttributeRoutes();

            AreaRegistration.RegisterAllAreas();
            
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            BundleConfig.Register(BundleTable.Bundles);

            ////common configuration 
            CommonConfigurations.ConfigureApp();
        }
    }
}