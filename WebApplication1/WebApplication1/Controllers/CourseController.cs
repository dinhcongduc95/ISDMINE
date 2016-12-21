using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CourseController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Course
        public ActionResult Index(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var course = db.Courses.Find(id);
            var user = db.Users.Find(User.Identity.GetUserId());
            var enrolment =
                db.Enrolments.Where(
                    m => m.User.Id.Equals(user.Id) && m.Course.CourseId.Equals(course.CourseId)).ToList();
            var exist = enrolment.Any();

            
            if (exist)
            {
                var lessions = db.Courses.Include(m => m.Lessions).ToList().Find(m => m.CourseId == id).Lessions;
                ViewBag.Course = db.Courses.Include(m => m.Platform).ToList().Find(m => m.CourseId == id);
                return View(lessions.ToList());
                             
            }
            return RedirectToAction("CourseRegister", new {id});

        }
     
        public ActionResult CourseRegister(int ? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userId = User.Identity.GetUserId();
            var course = db.Courses.Find(id);
;           var exist = db.Enrolments.Any(m => m.User.Id.Equals(userId) && m.Course.CourseId.Equals(course.CourseId));
            if (exist)
            {
                return RedirectToAction("YourCourse");
            }
            return View(db.Courses.Include(m => m.Platform).ToList().Find(m => m.CourseId == id));
        }

        [HttpPost]
        public ActionResult CourseRegister()
        {
            
            var courseId = Request.Form["CourseId"];
            if (string.IsNullOrEmpty(Request.Form["CourseId"]))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var course = db.Courses.Find(int.Parse(courseId));
            
            if (course != null && User.Identity.IsAuthenticated)
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                var enrolment = new Enrolment
                {
                    CreateDate = DateTime.Now.ToShortDateString(),
                    EndDate = DateTime.Now.AddDays(30).ToShortDateString(),
                    User = user,
                    Course = course,
                    IsValid = false
                };
                db.Enrolments.Add(enrolment);
                db.SaveChanges();
                return RedirectToAction("YourCourse");
            }
            return RedirectToAction("Login", "Account");            
        }

        public ActionResult YourCourse()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var userId = User.Identity.GetUserId();
            var enrolments = db.Enrolments.Include(m => m.User).Include(m => m.Course).Where(m => m.User.Id.Equals(userId));

            ViewBag.Enrolments = enrolments.ToList();

            return View();
        }
    }
}