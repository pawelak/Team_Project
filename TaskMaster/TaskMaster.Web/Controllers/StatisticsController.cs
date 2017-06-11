using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskMaster.BLL.WebServices;

namespace TaskMaster.Web.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly ActivityWebService _activityWebService = new ActivityWebService();
        // GET: Statistics
        public ActionResult Home()
        {
            var pieChart = _activityWebService.ActivityFromBase("dlanorberta@gmail.com");
            var Chart = _activityWebService.Last12MOfWork("dlanorberta@gmail.com");

            ViewBag.mod = pieChart;
            ViewBag.tab = Chart;
            return View();
        }
    }
}