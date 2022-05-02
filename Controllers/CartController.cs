﻿using System;
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
       
        public static void Addtocart(int id,int quantity)
        {
            
            CartController.placedOrder.Add(new List<int>{id, quantity});
            
        }
        public ActionResult Showcart()
        {
            ItemsinCart(placedOrder);
            return View();
        }
        //public void itemsincart()
        //{
        //    ItemsinCart(placedOrder);
        //}

        [HttpGet]
        public ActionResult ItemsinCart(List<List<int>> placedOrder)
        {
            List<Food_Items> foodItemOrdered = new List<Food_Items>();
            int vv = 0;
            for (int i = 0; i <placedOrder.Count(); i++)
            {
                vv = placedOrder[i][0];
                var m = (from obj in databaseEntityObject.Food_Items
                                   where obj.food_id == vv
                                   select obj).ToList();
              
                //ViewBag.foodItemOrdered = foodItemOrdered;
            }
            ViewBag.foodItemOrdered = foodItemOrdered;
            ViewBag.totalprice= getTotalCostOfOrder(placedOrder);
            return RedirectToAction("Showcart");
        }

        public ActionResult ConfirmCart()
        {
            return View();
        
        }
        [HttpPost]
        public ActionResult SaveCartToDataBase([Bind(Include = "order_id,Date,Total_cost,Status,user_email")] External_Order ExternalOrder)
        {
            Order_Details orderDetails = new Order_Details();
            External_Order lastOrderAdded = databaseEntityObject.External_Order.OrderByDescending(find => find.order_id).First();

            ExternalOrder.Date = DateTime.Now;
            ExternalOrder.Status = "Completed";
            ExternalOrder.Total_cost = getTotalCostOfOrder(placedOrder);
            databaseEntityObject.External_Order.Add(ExternalOrder);
            databaseEntityObject.SaveChanges();

            


            for (int i = 0; i < placedOrder.Count(); i++)
            {
                orderDetails.order_id = lastOrderAdded.order_id;
                orderDetails.food_id = placedOrder[i][0]; //index of [0] refers to id of food item
                
                orderDetails.quantity = placedOrder[i][1]; //index of [1] refers to quantity of food item
                Food_Items food = databaseEntityObject.Food_Items.Find(placedOrder[i][0]);
                orderDetails.price = food.price;
                orderDetails.Name = food.name;
                databaseEntityObject.Order_Details.Add(orderDetails);
                databaseEntityObject.SaveChanges();
            }


            return View();

        }

        public int getTotalCostOfOrder(List<List<int>>placedOrder)
        {
            int totalCost=0;
            for (int i = 0; i < placedOrder.Count(); i++)
            {
                totalCost += (placedOrder[i][1] * placedOrder[i][0]);
            }
            return totalCost;
        }
    }
}