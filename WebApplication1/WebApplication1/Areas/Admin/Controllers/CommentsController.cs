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
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Comments
        public ActionResult Index()
        {
            return View(db.Comments.Include(m => m.User).Include(m => m.Lession).ToList());
        }

        // GET: Admin/Comments/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Include(m => m.User).Include(m => m.Lession).ToList().Find(m => m.CommentId == id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Admin/Comments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentId,Content,Star")] Comment comment)
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

                comment.User = user;
                comment.Lession = lession;
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(comment);
        }

        // GET: Admin/Comments/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Include(m => m.User).Include(m => m.Lession).ToList().Find(m => m.CommentId == id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Admin/Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentId,Content,Star")] Comment comment)
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

                comment.User = user;
                comment.Lession = lession;
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comment);
        }

        // GET: Admin/Comments/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Include(m => m.User).Include(m => m.Lession).ToList().Find(m => m.CommentId == id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Admin/Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
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
