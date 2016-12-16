using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CourseController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Course
        public ActionResult Index(int? id=1)
        {
            ViewBag.Course = db.Courses.Find(id);
            return View();
        }

        public ActionResult Detail(int? id = 1)
        {
            ViewBag.Lessions = db.Lessions.Where(m => m.CourseId == id).ToList();
            ViewBag.CourseName = db.Courses.Find(id).Name;
            return View();
        }
    }
}