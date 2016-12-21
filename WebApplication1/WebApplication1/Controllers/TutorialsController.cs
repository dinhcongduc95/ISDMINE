using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TutorialsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Tutorials
        public ActionResult Index()
        {            
            return View(db.Tutorials.ToList());
        }
    }
}