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
            ViewBag.CourseEasy = db.Courses.Where(m => m.Level.Equals("Dễ")).ToList();
            ViewBag.CourseMedium = db.Courses.Where(m => m.Level.Equals("Trung bình")).ToList();
            ViewBag.CourseHard = db.Courses.Where(m => m.Level.Equals("Khó")).ToList();
           
            ViewBag.Title = "Course List";
            return View();
        }
    }
}