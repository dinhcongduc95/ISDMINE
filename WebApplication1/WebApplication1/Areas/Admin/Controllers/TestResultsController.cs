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
    [Authorize(Roles = "Admin")]
    public class TestResultsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/TestResults
        public ActionResult Index()
        {
            return View(db.TestResults.Include(m => m.User).Include(m => m.Test).ToList());
        }

        // GET: Admin/TestResults/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResult testResult = db.TestResults.Include(m => m.User).Include(m => m.Test).ToList().Find(m => m.TestResultId == id);
            if (testResult == null)
            {
                return HttpNotFound();
            }
            return View(testResult);
        }

        // GET: Admin/TestResults/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TestResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TestResultId,Mark,Passed")] TestResult testResult)
        {
            if (ModelState.IsValid)
            {
                // Get test
                var testStr = Request.Form["TestName"];
                if (string.IsNullOrEmpty(testStr))
                {
                    return RedirectToAction("Index");
                }
                var test = db.Tests.SingleOrDefault(m => m.Name.Equals(testStr));
                if (test == null)
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

                testResult.User = user;
                testResult.Test = test;
                db.TestResults.Add(testResult);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(testResult);
        }

        // GET: Admin/TestResults/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResult testResult = db.TestResults.Include(m => m.User).Include(m => m.Test).ToList().Find(m => m.TestResultId == id);
            if (testResult == null)
            {
                return HttpNotFound();
            }
            return View(testResult);
        }

        // POST: Admin/TestResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TestResultId,Mark,Passed")] TestResult testResult)
        {
            if (ModelState.IsValid)
            {
                // Get test
                var testStr = Request.Form["TestName"];
                if (string.IsNullOrEmpty(testStr))
                {
                    return RedirectToAction("Index");
                }
                var test = db.Tests.SingleOrDefault(m => m.Name.Equals(testStr));
                if (test == null)
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

                testResult.User = user;
                testResult.Test = test;
                db.Entry(testResult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(testResult);
        }

        // GET: Admin/TestResults/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResult testResult = db.TestResults.Include(m => m.User).Include(m => m.Test).ToList().Find(m => m.TestResultId == id);
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

        public ActionResult GetUsers([DataSourceRequest] DataSourceRequest request, string text)
        {
            var results = new List<UserViewModel>();          
            results = db.Users.Select(m => new UserViewModel
            {
                Username = m.UserName
            }).Where(m => m.Username.Contains(text)).ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTests([DataSourceRequest] DataSourceRequest request, string text)
        {
            var results = new List<TestViewModel>();            
            results = db.Tests.Select(m => new TestViewModel
            {
                Name = m.Name
            }).Where(m => m.Name.Contains(text)).ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}
