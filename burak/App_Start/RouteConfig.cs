using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace burak
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
            name: "kategoriler",
            url: "kategoriler/{id}",
            defaults: new { controller = "Home", action = "kategoriler" },
            namespaces: new string[] { "burak.Controllers" }
            );

            routes.MapRoute(
            name: "menu",
            url: "{id}",
            defaults: new { controller = "Home", action = "menu"},
            namespaces: new string[] { "burak.Controllers" }
            );

            routes.MapRoute(
            name: "hizmetler",
            url: "hizmetler/{id}",
            defaults: new { controller = "Home", action = "hizmetler" },
            namespaces: new string[] { "burak.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "burak.Controllers" }
            );
        }
    }
}
