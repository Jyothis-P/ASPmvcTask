using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task.Models;

namespace Task.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SignOut()
        {
            Session["UserId"] = null;
            Session["Username"] = null;
            return View();
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            return View(new UserInfo());
        }

        [HttpPost]
        public ActionResult SignIn(UserInfo objs)
        {

            if (!ModelState.IsValid)
                return View(objs);
            else
            {
                ExerciseDbEntities db = new ExerciseDbEntities();
                UserInfo user = (UserInfo)db.UserInfoes.Where(u => (u.Username == objs.Username) && (u.Userpwd == objs.Userpwd)).FirstOrDefault();
                if (user == null)
                {
                    // No User in the name.
                    ModelState.AddModelError("", "Invalid username or password.");
                }
                else
                {
                    // User Found
                    Session["UserId"] = Guid.NewGuid();
                    Session["Username"] = objs.Username;
                    return RedirectToAction("Index", "Home");
                }
                db.Dispose();
                return View();
            }
        }

    }
}