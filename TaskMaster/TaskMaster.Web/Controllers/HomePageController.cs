using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TaskMaster.BLL.WebServices;

namespace TaskMaster.Web.Controllers
{
    public class HomePageController : Controller
    {
        private readonly WebMainService _webMainService = new WebMainService();

        // GET: HomePage
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            if (userId != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userName = User.Identity.GetUserName();

                    return View();
                }
               
            }

            return RedirectToAction("Home","LandingPage");
        }
    }
}