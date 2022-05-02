using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_Food_Ordering_System.Models;

namespace Online_Food_Ordering_System.Controllers
{
    public class GetFoodCatecoriesController : Controller
    {
        // GET: GetFoodCatecories
        private FoodyDatabaseEntities db = new FoodyDatabaseEntities();  // will be changed to spacific name
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getFoodCategories()
        {
            return View(db.Categories);
        }
        [HttpGet]
        public ActionResult getFoodItems(int id)
        {
            List<Food_Items> foodItem = new List<Food_Items>();
            foodItem = (from obj in db.Food_Items
                        where obj.cat_type == id
                        select obj).ToList();
            ViewBag.foodItem = foodItem;
            return View();
        }

        public ActionResult calladdtocart(int id, int quantity)
        {
            CartController.Addtocart(id,quantity);
            return RedirectToAction("getFoodCategories");
        }


    }
}