using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class IntroController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Intro
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ClassSchedule()
        {
            ViewBag.Title = "Class Schedule";
            return View();
        }

        public ActionResult CourseList()
        {
            ViewBag.Courses = db.Courses.ToList();
            ViewBag.Title = "Course List";
            return View();
        }
    }
}