using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.Admin.Models.ViewModels;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Users
        public ActionResult Index()
        {
            var users = db.Users.Select(m => new UserViewModel
            {
                Username = m.UserName,
                Email = m.Email,
                Id = m.Id,
                //IsAdmin = m.Roles.Any(mRole => mRole.RoleId.Equals()),
            }).ToList();
            return View(users);
        }

                       

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(string id)
        {
            var users = db.Users.Select(m => new UserViewModel
            {
                Username = m.UserName,
                Email = m.Email,
                Id = m.Id
            }).SingleOrDefault(m => m.Id.Equals(id));
            return View(users);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
