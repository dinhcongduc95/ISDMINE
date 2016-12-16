using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    public class EnrolmentController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Enrolement
        public ActionResult Enrol(int id)
        {
            ViewBag.Course = _db.Courses.SingleOrDefault(m => m.CourseId == id);
            return View();
        }

        [HttpPost]
        public ActionResult Enrol(EnrolmentViewModel model)
        {

            //if (ModelState.IsValid)
            //{   

            //    var newEnrolment = new Enrolment();
            //    newEnrolment.IsValid = "true";
            //    newEnrolment.CreateDate = Request.Form["startDate"];
            //    newEnrolment.EndDate = Request.Form["endDate"];
            //    newEnrolment.UserIdRef = User.Identity.GetUserId();
            //    newEnrolment.CourseIdRef = Int32.Parse(Request.Form["courseId"]);     

            //    _db.Enrolments.Add(newEnrolment);
            //    _db.SaveChanges();
            //    return RedirectToAction("CourseList", "Intro");
            //}

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                
                if (_db != null)
                {
                    _db.Dispose();
                    _db = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}