using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Online_Food_Ordering_System.Models;

namespace Online_Food_Ordering_System.Controllers
{
    public class UsersController : PersonController
    {
        private FoodyDatabaseEntities databaseEntityObject = new FoodyDatabaseEntities();

        
        public ActionResult CreateUser()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser([Bind(Include = "id,name,pass,Type,img,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Type = "user";
                databaseEntityObject.Users.Add(user);
                databaseEntityObject.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(user);
        }

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
                var returnedUserAfterCheckingDatabase = databaseEntityObject.Users.Where(userRecord => userRecord.Email.Equals(user.Email) && userRecord.pass.Equals(user.pass)).FirstOrDefault();
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
                return NavigateToAnthorView("~/Views/Users/UserHome.cshtml");
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
