using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            
            ViewBag.Title = "Homepage";
            ViewBag.Message = "con điên"; 

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Title = "Homepage";
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Title = "Homepage";
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}