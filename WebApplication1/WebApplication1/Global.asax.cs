using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApplication1.Models;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var context = new ApplicationDbContext();

            if (!context.Roles.Any(role => role.Name == "SuperAdmin"))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                roleManager.Create(new IdentityRole("SuperAdmin"));
            }
            if (!context.Roles.Any(role => role.Name == "Teacher"))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                roleManager.Create(new IdentityRole("Teacher"));
            }
            if (!context.Roles.Any(role => role.Name == "Student"))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                roleManager.Create(new IdentityRole("Student"));
            }
            if (!context.Roles.Any(role => role.Name == "Admin"))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                roleManager.Create(new IdentityRole("Admin"));
            }

            if (!context.Users.Any(user => user.UserName == "admin@admin.com"))
            {
                var UserManagerFactory = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var user = new ApplicationUser { UserName = "admin@admin.com", Email = "admin@admin.com" };
                var result = UserManagerFactory.Create(user, "admin123");
                UserManagerFactory.AddToRole(user.Id, "Admin");
            }
            if (!context.Users.Any(user => user.UserName == "teacher@teacher.com"))
            {
                var UserManagerFactory = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var user = new ApplicationUser { UserName = "teacher@teacher.com", Email = "teacher@teacher.com" };
                var result = UserManagerFactory.Create(user, "teacher123");
                UserManagerFactory.AddToRole(user.Id, "Teacher");
            }
            if (!context.Users.Any(user => user.UserName == "student@student.com"))
            {
                var UserManagerFactory = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var user = new ApplicationUser { UserName = "student@student.com", Email = "student@student.com" };
                var result = UserManagerFactory.Create(user, "student123");
                UserManagerFactory.AddToRole(user.Id, "Student");
            }

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
