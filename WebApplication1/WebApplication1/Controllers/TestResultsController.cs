using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TestResultsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: TestResults
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();            
            return View(db.TestResults.Include(m => m.User).Include(m => m.Test).Where(m => m.User.Id.Equals(userId)).ToList());
        }
    }
}