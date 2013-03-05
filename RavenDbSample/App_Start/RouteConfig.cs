using System.Web.Mvc;
using System.Web.Routing;

namespace RavenDbSample
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{description}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, description = UrlParameter.Optional }
            );
        }
    }
}