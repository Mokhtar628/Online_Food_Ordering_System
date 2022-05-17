using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_Food_Ordering_System.Models;

namespace Online_Food_Ordering_System.Controllers
{
    public class GetTotalPriceController : Controller
    {
        private static FoodyDatabaseEntities databaseEntityObject = new FoodyDatabaseEntities();

        public static int getTotalCostOfOrder(List<List<int>> placedOrder)
        {
            int finalcost = 0;
            int? totalCost = 0;
            int indexOfplacedorder = 0;
            for (int i = 0; i < placedOrder.Count(); i++)
            {
                indexOfplacedorder = placedOrder[i][0];
                var foodOnCart = (from obj in databaseEntityObject.Food_Items
                                  where obj.food_id == indexOfplacedorder
                                  select obj).ToList()[0];
                totalCost += foodOnCart.price * placedOrder[i][1];

            }
            finalcost = totalCost.Value;
            return finalcost;
        }
    }
}