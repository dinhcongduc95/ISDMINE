using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using WebApplication1.Areas.Admin.Models.ViewModels;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class EnrolmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Enrolments
        public ActionResult Index()
        {
            return View(db.Enrolments.Include(m => m.Course).Include(m => m.User).ToList());
        }

        // GET: Admin/Enrolments/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrolment enrolment =
                db.Enrolments.Include(m => m.Course).Include(m => m.User).ToList().Find(m => m.EnrolmentId == id);
            if (enrolment == null)
            {
                return HttpNotFound();
            }
            return View(enrolment);
        }

        // GET: Admin/Enrolments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Enrolments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnrolmentId,CreateDate,EndDate,IsValid")] Enrolment enrolment)
        {
            if (ModelState.IsValid)
            {
                // Get course
                var courseStr = Request.Form["CourseName"];
                if (string.IsNullOrEmpty(courseStr))
                {
                    return RedirectToAction("Index");
                }
                var course = db.Courses.SingleOrDefault(m => m.Name.Equals(courseStr));
                if (course == null)
                {
                    return RedirectToAction("Index");
                }

                // Get user
                var userStr = Request.Form["Username"];
                if (string.IsNullOrEmpty(userStr))
                {
                    return RedirectToAction("Index");
                }
                var user = db.Users.SingleOrDefault(m => m.UserName.Equals(userStr));
                if (user == null)
                {
                    return RedirectToAction("Index");
                }

                enrolment.User = user;
                enrolment.Course = course;
                db.Enrolments.Add(enrolment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(enrolment);
        }

        // GET: Admin/Enrolments/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
                  
            Enrolment enrolment =
                db.Enrolments.Include(m => m.Course).Include(m => m.User).ToList().Find(m => m.EnrolmentId == id);
            if (enrolment == null)
            {
                return HttpNotFound();
            }
            return View(enrolment);
        }

        // POST: Admin/Enrolments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnrolmentId,CreateDate,EndDate,IsValid")] Enrolment enrolment)
        {
            if (ModelState.IsValid)
            {
                var courseStr = Request.Form["CourseName"];
                if (string.IsNullOrEmpty(courseStr))
                {
                    return RedirectToAction("Index");
                }
                var course = db.Courses.SingleOrDefault(m => m.Name.Equals(courseStr));
                if (course == null)
                {
                    return RedirectToAction("Index");
                }

                // Get user
                var userStr = Request.Form["Username"];
                if (string.IsNullOrEmpty(userStr))
                {
                    return RedirectToAction("Index");
                }
                var user = db.Users.SingleOrDefault(m => m.UserName.Equals(userStr));
                if (user == null)
                {
                    return RedirectToAction("Index");
                }

                enrolment.User = user;
                enrolment.Course = course;
                db.Entry(enrolment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(enrolment);
        }

        // GET: Admin/Enrolments/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrolment enrolment =
                db.Enrolments.Include(m => m.Course).Include(m => m.User).ToList().Find(m => m.EnrolmentId == id);
            if (enrolment == null)
            {
                return HttpNotFound();
            }
            return View(enrolment);
        }

        // POST: Admin/Enrolments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Enrolment enrolment = db.Enrolments.Find(id);
            db.Enrolments.Remove(enrolment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult GetUsers([DataSourceRequest] DataSourceRequest request, string text)
        {
           var results = new List<UserViewModel>();
            var test = db.Users.ToList();
            results = db.Users.Select(m => new UserViewModel
            {                
                Username = m.UserName               
            }).Where(m => m.Username.Contains(text)).ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCourses([DataSourceRequest] DataSourceRequest request, string text)
        {
            var results = new List<CourseViewModel>();

            results = db.Courses.Select(m => new CourseViewModel
            {
                Name = m.Name,
            }).Where(m => m.Name.Contains(text)).ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}
