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
    public class TestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Tests
        public ActionResult Index()
        {
            return View(db.Tests.Include(m => m.Lession).ToList());
        }

        // GET: Admin/Tests/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = db.Tests.Include(m => m.Lession).ToList().Find(m => m.TestId == id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
        }

        // GET: Admin/Tests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Tests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TestId,Name,XmlContent,TimeLimit,PassMark")] Test test)
        {
            if (ModelState.IsValid)
            {
                var lessionStr = Request.Form["LessionName"];
                if (string.IsNullOrEmpty(lessionStr))
                {
                    return RedirectToAction("Index");
                }
                var lession = db.Lessions.SingleOrDefault(m => m.Name.Equals(lessionStr));
                if (lession == null)
                {
                    return RedirectToAction("Index");
                }
                                
                test.Lession = lession;
                db.Tests.Add(test);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(test);
        }

        // GET: Admin/Tests/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = db.Tests.Include(m => m.Lession).ToList().Find(m => m.TestId == id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
        }

        // POST: Admin/Tests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TestId,Name,XmlContent,TimeLimit,PassMark")] Test test)
        {
            if (ModelState.IsValid)
            {
                var lessionStr = Request.Form["LessionName"];
                if (string.IsNullOrEmpty(lessionStr))
                {
                    return RedirectToAction("Index");
                }
                var lession = db.Lessions.SingleOrDefault(m => m.Name.Equals(lessionStr));
                if (lession == null)
                {
                    return RedirectToAction("Index");
                }

                test.Lession = lession;
                db.Entry(test).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(test);
        }

        // GET: Admin/Tests/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = db.Tests.Include(m => m.Lession).ToList().Find(m => m.TestId == id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
        }

        // POST: Admin/Tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Test test = db.Tests.Find(id);
            db.Tests.Remove(test);
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

        public ActionResult GetLessions([DataSourceRequest] DataSourceRequest request, string text)
        {
            var results = new List<LessionViewModel>();

            results = db.Lessions.Select(m => new LessionViewModel
            {
                Name = m.Name
            }).Where(m => m.Name.Contains(text)).ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}
