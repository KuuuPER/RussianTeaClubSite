using System.Web.Mvc;
using System.Web.Routing;

namespace RussianTeaClubSite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null,
                "",
                new
                {
                    controller = "Article",
                    action = "List",
                    page = 1,
                    item = "Домой" 
                }
            );

            routes.MapRoute(
                name: null,
                url: "{page}",
                defaults: new { controller = "Article", action = "List", page = 1, item = "Домой" },
                constraints: new { page = @"\d+" }
            );

            routes.MapRoute(
                name: null,
                url: "Page{page}",
                defaults: new { controller = "Article", action = "List", page = 1 },
                constraints: new { page = @"\d+" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Article", action = "List", item = "Домой", id = UrlParameter.Optional }
            );

            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}