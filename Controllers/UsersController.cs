using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Online_Food_Ordering_System.Models;

namespace Online_Food_Ordering_System.Controllers
{
    public class UsersController : PersonController, SessionsController
    {
        private FoodyDatabaseEntities databaseEntityObject = new FoodyDatabaseEntities();

        
        public ActionResult CreateUser()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser([Bind(Include = "id,name,pass,Type,img,Email,ImageFile")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Type = "user";
                user.img =  saveImage(user);
                databaseEntityObject.Users.Add(user);
                databaseEntityObject.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(user);
        }

        private String saveImage(User user)
        {
            string returnedFileName;
            if (object.ReferenceEquals(user.ImageFile, null))
            {
                returnedFileName = "~/UsersImage/profImg.png";

            }
            else
            {
                string fileName = Path.GetFileNameWithoutExtension(user.ImageFile.FileName);
                string extension = Path.GetExtension(user.ImageFile.FileName);
                fileName = DateTime.Now.ToString("yymmssfff") + fileName + extension;
                returnedFileName = "~/UsersImage/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/UsersImage/"), fileName);
                user.ImageFile.SaveAs(fileName);
            }
            return returnedFileName;
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
                    new ErrorMessagesController().DisplayWrongDataError();
                }
            }
            return View(user);
        }

        override
        public ActionResult GoToHome()
        {
            if (Session["ID"] != null)
            {
                return RedirectToAction("getFoodCategories", "GetFoodCatecories");
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

        public void OpenSessions(User foodyDatabaseEntities)
        {
            Session["id"] = foodyDatabaseEntities.id.ToString();
            Session["name"] = foodyDatabaseEntities.name.ToString();
            Session["img"] = foodyDatabaseEntities.img.ToString();
        }

        public ActionResult ViewAllUsers()
        {
            return View(databaseEntityObject.Users);
        }

    }
}
