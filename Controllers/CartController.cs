using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_Food_Ordering_System.Models;


namespace Online_Food_Ordering_System.Controllers
{
    public  class CartController : Controller
    {
        private FoodyDatabaseEntities databaseEntityObject = new FoodyDatabaseEntities();
        public static List<List<int>> placedOrder = new List<List<int>>();
        public static List<Food_Items> foodItemOrdered = new List<Food_Items>();


        public ActionResult Addtocart(int id,int quantity)
        {
            
            CartController.placedOrder.Add(new List<int>{id, quantity});
            return RedirectToAction("getFoodCategories","GetFoodCatecories");

        }


        [HttpGet] 
        public ActionResult ItemsinCart(List<List<int>> placedOrder)
        {
            int indexOfplacedorder = 0;
            for (int i = 0; i < placedOrder.Count(); i++)
            {
                indexOfplacedorder = placedOrder[i][0];
                var foodOnCart = (from obj in databaseEntityObject.Food_Items
                                  where obj.food_id == indexOfplacedorder
                                  select obj).ToList()[0];
                foodItemOrdered.Add(foodOnCart);
            }

            ViewBag.foodItemOrdered = foodItemOrdered;
            ViewBag.totalprice = GetTotalPriceController.getTotalCostOfOrder(placedOrder);

            return RedirectToAction("Showcart");
        }


        public ActionResult Showcart()
        {
            ItemsinCart(placedOrder);
            return View();
        }
       

       

        
        public ActionResult SaveCartToDataBase()
        {
            Order_Details orderDetails = new Order_Details();
            External_Order ExternalOrder = new External_Order();
           

            ExternalOrder.user_email = Session["Email"].ToString();
            ExternalOrder.Date = DateTime.Now;
            ExternalOrder.Status = "Completed";
            ExternalOrder.Total_cost = GetTotalPriceController.getTotalCostOfOrder(placedOrder);
            databaseEntityObject.External_Order.Add(ExternalOrder);
            databaseEntityObject.SaveChanges();


            External_Order lastOrderAdded = databaseEntityObject.External_Order.OrderByDescending(find => find.order_id).First();

            for (int i = 0; i < placedOrder.Count(); i++)
            {
                orderDetails.order_id = lastOrderAdded.order_id;
                orderDetails.food_id = placedOrder[i][0]; //index of [0] refers to id of food item
                orderDetails.quantity = placedOrder[i][1]; //index of [1] refers to quantity of food item
                Food_Items food = databaseEntityObject.Food_Items.Find(placedOrder[i][0]);
                orderDetails.price = food.price;
                orderDetails.Name = food.name;
                databaseEntityObject.Order_Details.Add(orderDetails);
            }
            databaseEntityObject.SaveChanges();
            foodItemOrdered.Clear();
            placedOrder.Clear();

            return View();

        }



        public ActionResult ConfirmCart()
        {
            return RedirectToAction("SaveCartToDataBase");

        }



       
    }
}