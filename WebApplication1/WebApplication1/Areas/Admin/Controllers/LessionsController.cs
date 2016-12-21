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
    public class LessionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Lessions
        public ActionResult Index()
        {
            return View(db.Lessions.Include(m => m.Course).ToList());
        }

        // GET: Admin/Lessions/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lession lession = db.Lessions.Include(m => m.Course).ToList().Find(m => m.LesionId == id);
            if (lession == null)
            {
                return HttpNotFound();
            }
            return View(lession);
        }

        // GET: Admin/Lessions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Lessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LesionId,Name,Description,YoutubeLink,ImageLink")] Lession lession)
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
                course.Lessions.Add(lession);

                db.Lessions.Add(lession);
                db.Entry(course).State = EntityState.Modified;                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lession);
        }

        // GET: Admin/Lessions/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lession lession = db.Lessions.Include(m => m.Course).ToList().Find(m => m.LesionId == id);
            if (lession == null)
            {
                return HttpNotFound();
            }
            return View(lession);
        }

        // POST: Admin/Lessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LesionId,Name,Description,YoutubeLink,ImageLink")] Lession lession)
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

                var curLession = db.Lessions.Include(m => m.Course).ToList().Find(m => m.LesionId == lession.LesionId);

                curLession.Description = lession.Description;
                curLession.ImageLink = lession.ImageLink;
                curLession.Name = lession.Name;
                curLession.YoutubeLink = lession.YoutubeLink;
                curLession.Course = course;
                
                db.Entry(curLession).State = EntityState.Modified;
                                               
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lession);
        }

        // GET: Admin/Lessions/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lession lession = db.Lessions.Include(m => m.Course).ToList().Find(m => m.LesionId == id);
            if (lession == null)
            {
                return HttpNotFound();
            }
            return View(lession);
        }

        // POST: Admin/Lessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Lession lession = db.Lessions.Find(id);
            db.Lessions.Remove(lession);
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

        public ActionResult ListAllComments(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lession lession = db.Lessions.Include(m => m.Comments).ToList().Find(m => m.LesionId == id);
            if (lession == null)
            {
                return HttpNotFound();
            }
            var allComments = lession.Comments;
            ViewBag.LessionId = id;
            ViewBag.LessionName = lession.Name;
            return View(allComments);
        }

        public ActionResult ListAllTests(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lession lession = db.Lessions.Include(m => m.Comments).ToList().Find(m => m.LesionId == id);
            if (lession == null)
            {
                return HttpNotFound();
            }
            var allTests = lession.Tests;
            ViewBag.LessionId = id;
            ViewBag.LessionName = lession.Name;
            return View(allTests);
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
