using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class TestResultsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/TestResults
        public ActionResult Index()
        {
            var testResults = db.TestResults.Include(t => t.Test).Include(t => t.User);
            return View(testResults.ToList());
        }

        // GET: Admin/TestResults/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResult testResult = db.TestResults.Find(id);
            if (testResult == null)
            {
                return HttpNotFound();
            }
            return View(testResult);
        }

        // GET: Admin/TestResults/Create
        public ActionResult Create()
        {
            ViewBag.TestId = new SelectList(db.Tests, "TestId", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "HomeTown");
            return View();
        }

        // POST: Admin/TestResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TestResultId,TestId,UserId,Mark,Passed")] TestResult testResult)
        {
            if (ModelState.IsValid)
            {
                db.TestResults.Add(testResult);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TestId = new SelectList(db.Tests, "TestId", "Name", testResult.TestId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "HomeTown", testResult.UserId);
            return View(testResult);
        }

        // GET: Admin/TestResults/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResult testResult = db.TestResults.Find(id);
            if (testResult == null)
            {
                return HttpNotFound();
            }
            ViewBag.TestId = new SelectList(db.Tests, "TestId", "Name", testResult.TestId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "HomeTown", testResult.UserId);
            return View(testResult);
        }

        // POST: Admin/TestResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TestResultId,TestId,UserId,Mark,Passed")] TestResult testResult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testResult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TestId = new SelectList(db.Tests, "TestId", "Name", testResult.TestId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "HomeTown", testResult.UserId);
            return View(testResult);
        }

        // GET: Admin/TestResults/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResult testResult = db.TestResults.Find(id);
            if (testResult == null)
            {
                return HttpNotFound();
            }
            return View(testResult);
        }

        // POST: Admin/TestResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            TestResult testResult = db.TestResults.Find(id);
            db.TestResults.Remove(testResult);
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
    }
}
