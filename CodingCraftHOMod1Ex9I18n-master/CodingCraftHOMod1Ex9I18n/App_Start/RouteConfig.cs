using System.Web.Mvc;
using System.Web.Routing;

namespace CodingCraftHOMod1Ex9I18n
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
           
            routes.MapRoute(
                name: "DefaultLocalized",
                url: "{culture}/{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional,
                    culture = "pt-BR"
                },
                constraints: new { culture = "[a-z]{2}-[A-Z]{2}" }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
