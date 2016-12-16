using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo");

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "WebApplication1.Controllers" }
            );

            routes.MapRoute(
                "UserIndex",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index"},
                new [] { "WebApplication1.Controllers" }
            );

            routes.MapRoute(
                "AdminIndex",
                "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", AreaName = "Admin" },     
                new[] {"WebApplication1.Areas.Admin.Controllers"}
            );
        }
    }
}
