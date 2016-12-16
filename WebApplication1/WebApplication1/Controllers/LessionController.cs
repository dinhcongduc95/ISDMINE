using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
                ViewBag.Lession = db.Lessions.Include(l => l.Comments).Include(m => m.Tests).First();
            }
            else
            {
                ViewBag.Lession = db.Lessions.Include(l => l.Comments).Include(m => m.Tests).SingleOrDefault(m => m.LesionId == id);
            }
            
            
            return View();
        }

        public JsonResult AddComment()
        {
            
            
            var lessionId = Request["lessionId"];
            var commentContent = Request["content"];
            
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new {result = "forbiden"});
            }

            db.Comments.Add(new Comment
            {
                UserId = User.Identity.GetUserId(),
                LessionId = int.Parse(lessionId),
                Content = commentContent
            });
            db.SaveChanges();

            var commentId = db.Comments.OrderBy(m => m.CommentId).First().CommentId;
            
            
            return Json(new {content = commentContent, commentId});
        }
        
    }
}