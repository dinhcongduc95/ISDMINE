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
    public class RatingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Ratings
        public ActionResult Index()
        {
            var ratings = db.Ratings.Include(r => r.Lession).Include(r => r.User);
            return View(ratings.ToList());
        }

        // GET: Admin/Ratings/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // GET: Admin/Ratings/Create
        public ActionResult Create()
        {
            ViewBag.LessionId = new SelectList(db.Lessions, "LesionId", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "HomeTown");
            return View();
        }

        // POST: Admin/Ratings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RatingId,LessionId,UserId,Content,Point")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Ratings.Add(rating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LessionId = new SelectList(db.Lessions, "LesionId", "Name", rating.LessionId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "HomeTown", rating.UserId);
            return View(rating);
        }

        // GET: Admin/Ratings/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            ViewBag.LessionId = new SelectList(db.Lessions, "LesionId", "Name", rating.LessionId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "HomeTown", rating.UserId);
            return View(rating);
        }

        // POST: Admin/Ratings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RatingId,LessionId,UserId,Content,Point")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LessionId = new SelectList(db.Lessions, "LesionId", "Name", rating.LessionId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "HomeTown", rating.UserId);
            return View(rating);
        }

        // GET: Admin/Ratings/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // POST: Admin/Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Rating rating = db.Ratings.Find(id);
            db.Ratings.Remove(rating);
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
