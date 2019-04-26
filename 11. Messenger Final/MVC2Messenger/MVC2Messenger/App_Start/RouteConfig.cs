using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC2Messenger
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "join",
                url: "join/{id}",
                defaults: new { controller = "ChatUser", action = "Create" }
            );

            routes.MapRoute(
                name: "create",
                url: "create-{controller}",
                defaults: new {action = "Create" }
            );

            routes.MapRoute(
                name: "Index",
                url: "{controller}s",
                defaults: new { action = "Index" }
               );

            routes.MapRoute(
                name: "Find",
                url: "{controller}s/{name}",
                defaults: new { action = "Find" }
               );

            routes.MapRoute(
                name: "Details",
                url: "{controller}/{id}",
                defaults: new { action = "Details" },
                constraints: new { id = @"\d+" }
               );

            routes.MapRoute(
                name: "EditDelete",
                url: "{controller}/{id}/{action}",
                defaults: new { },
                constraints: new { id = @"\d+" }
               );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Chat", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
