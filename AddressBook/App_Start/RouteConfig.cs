using System.Web.Mvc;
using System.Web.Routing;

namespace AddressBook
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Default1",
                url: "Web1/{controller}/{action}/{id}",
                defaults: new { controller = "Web1", action = "Users", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default2",
                url: "Web2/{controller}/{action}/{id}",
                defaults: new { controller = "Web2", action = "Users", id = UrlParameter.Optional }
            );
        }
    }
}
