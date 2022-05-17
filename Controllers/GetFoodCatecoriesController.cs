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
        private FoodyDatabaseEntities databaseEntityObject = new FoodyDatabaseEntities();
        
       
        public ActionResult getFoodCategories()
        {
            return View(databaseEntityObject.Categories);
        }
       

        //public ActionResult calladdtocart(int id, int quantity)
        //{
        //    CartController.Addtocart(id, quantity);
        //    return RedirectToAction("getFoodCategories");
        //}


    }
}