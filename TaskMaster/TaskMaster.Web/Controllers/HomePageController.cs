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
        private readonly ActivityWebService _activityWebService = new ActivityWebService();

        // GET: HomePage
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            //if (userId != null)
            //{
            //    if (User.Identity.IsAuthenticated)
            //    {

                    var tmpLog = User.Identity.Name.ToString();
                    var lastMonth = _activityWebService.LastMonth(tmpLog, 30);
                    var longestTask = _activityWebService.LongestTask(tmpLog);
                    var longestTaskToChart = _activityWebService.LongestTaskToChart(tmpLog, 30);
    
                    ViewBag.mod = lastMonth;
                    ViewBag.tab = longestTask;
                    ViewBag.alt = longestTaskToChart;
                    return View();
            //    }
               
            //}

            //return RedirectToAction("Home","LandingPage");
        }

        public ActionResult MartynasNewIdea() {
            return View();
        }
    }
}