using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TaskMaster.BLL.WebModels;
using TaskMaster.BLL.WebServices;

namespace TaskMaster.Web.Controllers
{
    public class HomePageController : Controller
    {
        private readonly WebMainService _webMainService = new WebMainService();

        // GET: HomePage
        public ActionResult Index()
        {
            foreach (var s in _webMainService.ShowActivity("dlanorberta@gmail.com"))
            {
                s.Name.ToString();
            }
            return View();
        }

        public ActionResult MartynasNewIdea() {
            return View();
        }
    }
}