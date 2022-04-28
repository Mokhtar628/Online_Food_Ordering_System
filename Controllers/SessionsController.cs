using Online_Food_Ordering_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Food_Ordering_System.Controllers
{
    interface SessionsController
    {
        void OpenSessions(User foodyDatabaseEntities);
    }
}