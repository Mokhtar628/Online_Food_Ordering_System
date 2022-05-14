using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Runtime.CompilerServices;
using Online_Food_Ordering_System.Models;

namespace Online_Food_Ordering_System.Controllers
{
    public class Food_ItemsController : Controller
    {
        private FoodyDatabaseEntities db = new FoodyDatabaseEntities();

        public ActionResult List_food()
        {
            var food_Items = db.Food_Items.Include(f => f.Category);
            return View(food_Items.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.cat_type = new SelectList(db.Categories, "cat_id", "name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase ImageFile,[Bind(Include = "food_id,name,price,preparing_time,description,available,img,cat_type")] Food_Items food_Items)
        {

            if (ModelState.IsValid)
            {
                food_Items.img = saveImage(ImageFile);
                db.Food_Items.Add(food_Items);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cat_type = new SelectList(db.Categories, "cat_id", "name", food_Items.cat_type);
            return View(food_Items);
        }

        private string saveImage(HttpPostedFileBase ImageFile)
        {    
            string returnedFileName;
            if (object.ReferenceEquals(ImageFile, null))
            {
                returnedFileName = "~/UsersImage/profImg.png";

            }
            else
            {
                string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                string extension = Path.GetExtension(ImageFile.FileName);
                fileName = DateTime.Now.ToString("yymmssfff") + fileName + extension;
                returnedFileName = "~/Food/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Food/"), fileName);
                ImageFile.SaveAs(fileName);
            }
            return returnedFileName;
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food_Items food_Items = db.Food_Items.Find(id);
            if (food_Items == null)
            {
                return HttpNotFound();
            }
            ViewBag.cat_type = new SelectList(db.Categories, "cat_id", "name", food_Items.cat_type);
            return View(food_Items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase ImageFile,[Bind(Include = "food_id,name,price,preparing_time,description,available,img,cat_type")] Food_Items food_Items)
        {
            if (ModelState.IsValid)
            {
                food_Items.img = saveImage(ImageFile);
                db.Entry(food_Items).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cat_type = new SelectList(db.Categories, "cat_id", "name", food_Items.cat_type);
            return View(food_Items);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food_Items food_Items = db.Food_Items.Find(id);
            if (food_Items == null)
            {
                return HttpNotFound();
            }
            return View(food_Items);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Food_Items food_Items = db.Food_Items.Find(id);
            db.Food_Items.Remove(food_Items);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SearchByType(int type)
        {

            var food_Items = db.Food_Items.Include(f => f.Category);
            return View(db.Food_Items.Where(r => r.cat_type ==type).ToList());
            
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
