using Online_Food_Ordering_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Food_Ordering_System.Controllers
{
    public class AdminController : PersonController
    {
        private FoodyDatabaseEntities databaseEntityObject = new FoodyDatabaseEntities();


        override
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        override
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                var returnedUserAfterCheckingDatabase = databaseEntityObject.Users.Where(userRecord => userRecord.Email.Equals(user.Email) && userRecord.pass.Equals(user.pass) && userRecord.Type.Equals("admin")).FirstOrDefault();
                if (returnedUserAfterCheckingDatabase != null)
                {
                    OpenSessions(returnedUserAfterCheckingDatabase);
                    return RedirectToAction("GoToHome");
                }
                else
                {
                    DisplayWrongDataError();
                }
            }
            return View(user);
        }

        

        override
        public ActionResult GoToHome()
        {
            if (Session["ID"] != null)
            {
                return NavigateToAnthorView("~/Views/Admin/AdminHome.cshtml");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                databaseEntityObject.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}