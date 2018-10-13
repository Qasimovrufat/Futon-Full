using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Futon
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            

            routes.MapRoute(
         name: "Product",
         url: "Product/{id}/{name}",
         defaults: new { controller = "product", action = "index", id = UrlParameter.Optional, name = UrlParameter.Optional },
          namespaces: new[] { "Futon.Controllers" });

            routes.MapRoute(
         name: "Category",
         url: "category/{id}/{name}",
         defaults: new { controller = "category", action = "index", id = UrlParameter.Optional, name=UrlParameter.Optional },
          namespaces: new[] { "Futon.Controllers" }
        );

            routes.MapRoute(
             name: "Department",
             url: "department/{id}",
             defaults: new { controller = "department", action = "index", id = UrlParameter.Optional },
              namespaces: new[] { "Futon.Controllers" }
         );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "home", action = "index", id = UrlParameter.Optional },
                namespaces : new [] { "Futon.Controllers" }

            );
        }
    }
}
