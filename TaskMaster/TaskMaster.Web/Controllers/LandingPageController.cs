using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskMaster.Web.Resources;

namespace TaskMaster.Web.Controllers
{
    public class LandingPageController : Controller
    {
        // GET: LandingPage
        public ActionResult Index()
        {
        

            return View();
        }
    }
}