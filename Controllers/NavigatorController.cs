using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Food_Ordering_System.Controllers
{
    public class NavigatorController : Controller
    {
        public ActionResult NavigateToAnthorView(String viewPath)
        {
            return View(viewPath);
        }
    }
}