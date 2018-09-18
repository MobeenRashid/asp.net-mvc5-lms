using System.Web.Mvc;

namespace Debugtime.Helpers.Html
{
    public static class HelperExtensions
    {
        public static MvcHtmlString Submit(this HtmlHelper helper, string id="Submit", string value = "Submit",dynamic htmlAttributes=null)
        {
            return new MvcHtmlString($"<input type=\"submit\" id={id} class={htmlAttributes?.@class} value=\"{value}\"/>");
        }
    }
}