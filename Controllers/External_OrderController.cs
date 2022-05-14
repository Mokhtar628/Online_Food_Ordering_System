using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Online_Food_Ordering_System.Models;

namespace Online_Food_Ordering_System.Controllers
{
    public class External_OrderController : Controller
    {
        private FoodyDatabaseEntities db = new FoodyDatabaseEntities();

        // GET: External_Order
        public ActionResult List_orders()
        {
            var external_Order = db.External_Order.Include(e => e.User);
            return View(external_Order.ToList());
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
