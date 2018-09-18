using System.Web.Mvc;

namespace Debugtime.Areas.Cart
{
    public class KartAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "Cart";

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Cart_default",
                "Cart/{controller}/{action}/{id}",
                new { action = "ConfirmPurchase", id = UrlParameter.Optional }
            );
        }
    }
}