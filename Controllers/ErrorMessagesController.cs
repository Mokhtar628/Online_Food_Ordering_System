using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Food_Ordering_System.Controllers
{
    public class ErrorMessagesController : Controller
    {
        public void DisplayWrongDataError()
        {
            ModelState.AddModelError("LoginError", "Sorry, you entered incorrect data.");
        }
    }
}