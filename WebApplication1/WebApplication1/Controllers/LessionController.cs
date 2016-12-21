using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LessionController : Controller
    {
        private ApplicationDbContext db =  new ApplicationDbContext();
        // GET: Lession
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            ViewBag.Lession = db.Lessions.Include(l => l.Comments).Include(m => m.Tests).SingleOrDefault(m => m.LesionId == id);
            ViewBag.Comments = db.Comments.Include(m => m.User).Where(m => m.Lession.LesionId == id).ToList();
            ViewBag.Tests = db.Tests.Include(m => m.Lession).Where(m => m.Lession.LesionId == id);
            return View();
        }

        
        public JsonResult AddComment()
        {
            var lessionId = int.Parse(Request["lessionId"]);
            var commentContent = HttpUtility.UrlDecode(Request["content"]);
            
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new {result = "forbiden"});
            }

            var lession = db.Lessions.Find(lessionId);
            if (lession == null)
            {
                return null;
            }
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            db.Comments.Add(new Comment
            {
                User = user,
                Lession = lession,
                Star = 5,
                Content = commentContent
            });
            db.SaveChanges();

            var commentId = db.Comments.OrderBy(m => m.CommentId).First().CommentId;

            return Json(new {content = commentContent, commentId});
        }
        
    }
}