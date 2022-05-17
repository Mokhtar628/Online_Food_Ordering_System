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
       

 


    }
}