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
        private FoodyDatabaseEntities databaseEntities = new FoodyDatabaseEntities();

        public ActionResult List_food()
        {
            var food_Items = databaseEntities.Food_Items.Include(f => f.Category);
            return View(food_Items.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.cat_type = new SelectList(databaseEntities.Categories, "cat_id", "name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase ImageFile,[Bind(Include = "food_id,name,price,preparing_time,description,available,img,cat_type")] Food_Items food_Items)
        {

            if (ModelState.IsValid)
            {
                food_Items.img = saveImage(ImageFile);
                databaseEntities.Food_Items.Add(food_Items);
                databaseEntities.SaveChanges();
                return RedirectToAction("List_food");
            }

            ViewBag.cat_type = new SelectList(databaseEntities.Categories, "cat_id", "name", food_Items.cat_type);
            return View(food_Items);
        }

        private string saveImage(HttpPostedFileBase ImageFile)
        {    
            string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
            string extension = Path.GetExtension(ImageFile.FileName);
            fileName = DateTime.Now.ToString("yymmssfff") + fileName + extension;
            string returnedFileName = "~/Food/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Food/"), fileName);
            ImageFile.SaveAs(fileName);
            return returnedFileName;
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food_Items food_Items = databaseEntities.Food_Items.Find(id);
            if (food_Items == null)
            {
                return HttpNotFound();
            }
            ViewBag.cat_type = new SelectList(databaseEntities.Categories, "cat_id", "name", food_Items.cat_type);
            return View(food_Items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase ImageFile,[Bind(Include = "food_id,name,price,preparing_time,description,available,img,cat_type")] Food_Items food_Items)
        {
            if (ModelState.IsValid)
            {
                food_Items.img = saveImage(ImageFile);
                databaseEntities.Entry(food_Items).State = EntityState.Modified;
                databaseEntities.SaveChanges();
                return RedirectToAction("List_food");
            }
            ViewBag.cat_type = new SelectList(databaseEntities.Categories, "cat_id", "name", food_Items.cat_type);
            return View(food_Items);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food_Items food_Items = databaseEntities.Food_Items.Find(id);
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
            Food_Items food_Items = databaseEntities.Food_Items.Find(id);
            databaseEntities.Food_Items.Remove(food_Items);
            databaseEntities.SaveChanges();
            return RedirectToAction("List_food");
        }

        public ActionResult SearchByType(int type)
        {

            var food_Items = databaseEntities.Food_Items.Include(f => f.Category);
            return View(databaseEntities.Food_Items.Where(r => r.cat_type ==type).ToList());
            
        }




        [HttpGet]
        public ActionResult getFoodItems(int id)
        {
            List<Food_Items> foodItem = new List<Food_Items>();
            foodItem = (from obj in databaseEntities.Food_Items
                        where obj.cat_type == id
                        select obj).ToList();
            ViewBag.foodItem = foodItem;
            return View();
        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                databaseEntities.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
