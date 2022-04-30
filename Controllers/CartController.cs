using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Food_Ordering_System.Controllers
{
    public  class CartController 
    {
        public static List<int> foodid = new List<int>();
        public static void Addtocart(int id)
        {
            CartController.foodid.Add(id);           
        }

    }
}