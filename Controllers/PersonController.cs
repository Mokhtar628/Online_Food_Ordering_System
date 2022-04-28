using Online_Food_Ordering_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Food_Ordering_System.Controllers
{
    public abstract class PersonController : Controller
    {
        public abstract ActionResult Login();
        public abstract ActionResult Login(User obj);
        public abstract ActionResult GoToHome();
        
    }
}