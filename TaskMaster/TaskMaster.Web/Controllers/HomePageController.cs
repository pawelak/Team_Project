using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace TaskMaster.Web.Controllers
{
    public class HomePageController : Controller
    {
        private readonly WebMainService _webMainService = new WebMainService();

        // GET: HomePage
        public ActionResult Index()
        {
            return View();
        }
    }
}