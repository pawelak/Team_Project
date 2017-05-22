using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskMaster.Web.Controllers
{
    public class CallendarController : Controller
    {
        // GET: Callendar
        public ActionResult Home()
        {
            return View();
        }
    }
}