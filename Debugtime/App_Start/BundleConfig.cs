using System.Web.Optimization;

namespace Debugtime
{
    public class BundleConfig
    {
        public static void Register(BundleCollection bundle)
        {
            bundle.Add(new StyleBundle("~/Content/Styles").
                Include("~/Content/Styles/foundation.css",
                "~/Content/Font_Awesome/css/font-awesome.css",
                "~/Content/Styles/app.css"));

            bundle.Add(new StyleBundle("~/Content/Styles/MediaPlayer").
                Include("~/Content/Styles/media-player.css"));

            bundle.Add(new ScriptBundle("~/Content/Scripts").
                Include("~/Content/Scripts/foundation.js",
                "~/Content/Scripts/jquery-{version}.js",
                "~/Content/Scripts/what-input.js",
                "~/Content/Scripts/app.js",
                "~/Content/Scripts/MediaController.js"));
            
        }
    }
}