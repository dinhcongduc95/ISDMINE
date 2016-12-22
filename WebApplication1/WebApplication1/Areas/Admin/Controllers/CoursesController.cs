using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using WebApplication1.Areas.Admin.Models.ViewModels;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Courses
        public ActionResult Index()
        {
            var test = db.Courses.Include(m => m.Platform).ToList();
            return View(db.Courses.ToList());
        }

        // GET: Admin/Courses/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var course = db.Courses.Include(m => m.Platform).ToList().Find(c => c.CourseId == id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Admin/Courses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "CourseId,Name,Introduction,TuitionFee,EntryTestXml,TimeLimit,PassMark,ImageLink,Level")] Course course)
        {
            if (ModelState.IsValid)
            {
                var platformStr = Request.Form["Platform"];
                if (string.IsNullOrEmpty(platformStr))
                {
                    return RedirectToAction("Index");
                }
                var platform = db.Platforms.SingleOrDefault(m => m.Name.Equals(platformStr));
                if (platform == null)
                {
                    return RedirectToAction("Index");
                }
                platform.Courses.Add(course);
                db.Courses.Add(course);
                db.Entry(platform).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: Admin/Courses/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var course = db.Courses.Include(m => m.Platform).ToList().Find(c => c.CourseId == id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Admin/Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "CourseId,Name,Introduction,TuitionFee,EntryTestXml,TimeLimit,PassMark,ImageLink,Level")] Course course)
        {
            if (ModelState.IsValid)
            {
                var platformStr = Request.Form["PlatformName"];
                if (string.IsNullOrEmpty(platformStr))
                {
                    return RedirectToAction("Index");
                }
                var platform = db.Platforms.SingleOrDefault(m => m.Name.Equals(platformStr));
                if (platform == null)
                {
                    return RedirectToAction("Index");
                }
                var curCourse = db.Courses.Include(m => m.Platform).ToList().Find(m => m.CourseId == course.CourseId);

                curCourse.EntryTestXml = course.EntryTestXml;
                curCourse.ImageLink = course.ImageLink;
                curCourse.Introduction = course.Introduction;
                curCourse.Level = course.Level;
                curCourse.Name = course.Name;
                curCourse.PassMark = course.PassMark;
                curCourse.TimeLimit = course.TimeLimit;
                curCourse.TuitionFee = course.TuitionFee;
                curCourse.Platform = platform;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Admin/Courses/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var course = db.Courses.Include(m => m.Platform).ToList().Find(c => c.CourseId == id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Admin/Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            var course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ListAllLessions(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var course = db.Courses.Include(m => m.Lessions).ToList().Find(m => m.CourseId == id);
            if (course == null)
            {
                return HttpNotFound();
            }
            var allCourses = course.Lessions;
            ViewBag.CourseId = id;
            ViewBag.CourseName = course.Name;
            return View(allCourses);
        }

        public ActionResult ListAllEnrolments(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var course = db.Courses.Include(m => m.Enrolments).ToList().Find(m => m.CourseId == id);
            if (course == null)
            {
                return HttpNotFound();
            }
            var allEnrolemnts = course.Enrolments;
            ViewBag.CourseId = id;
            ViewBag.CourseName = course.Name;
            return View(allEnrolemnts);
        }

        public ActionResult RemoveLessionFromCourse(long? id, long? courseId)
        {
            if (id == null || courseId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var course = db.Courses.Include(m => m.Lessions).ToList().Find(m => m.CourseId == courseId);
            if (course == null)
            {
                return HttpNotFound();
            }
            var lession = db.Lessions.Find(id);
            if (lession == null)
            {
                return HttpNotFound();
            }
            course.Lessions.Remove(lession);
            db.Entry(course).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ListAllLessions", new {id = courseId});
        }

        public ActionResult GetPlatforms([DataSourceRequest] DataSourceRequest request, string text)
        {
            var results = new List<PlatformViewModel>();

            results = db.Platforms.Select(m => new PlatformViewModel
            {
                Description = m.Description,
                Name = m.Name,
                PlatformId = m.PlatformId
            }).Where(m => m.Name.Contains(text)).ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
