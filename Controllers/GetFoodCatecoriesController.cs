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
        public ActionResult Index()
        {
            return View();
        }
        public List<string> getFoodCategories(Category category)
        {
            List<string> foodCategories = new List<string>();
            return foodCategories;
        }
    }
}