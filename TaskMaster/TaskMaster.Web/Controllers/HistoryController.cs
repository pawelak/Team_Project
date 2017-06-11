using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskMaster.BLL.WebServices;

namespace TaskMaster.Web.Controllers
{
    public class HistoryController : Controller
    {
        // GET: History
        public ActionResult Home()
        {
            WebMainService hist = new WebMainService();
            ViewBag.mod = hist.History("dlanorberta@gmail.com");

            return View();
        }

        public ActionResult Edit() {
            return View();
        }
    }
}