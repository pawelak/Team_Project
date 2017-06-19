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
        public List<string> y = new List<string>();
        // GET: History
        public ActionResult Home()
        {
            WebMainService hist = new WebMainService();
            ViewBag.mod = hist.History("dlanorberta@gmail.com");

          //  y = new List<string> {(TempData["Data1"]).ToString()};

            return View();
        }


        public ActionResult Edit()
        {
            WebHistEditService _webHistEditService = new WebHistEditService();
            var taskEdit = _webHistEditService.ShowEditTask("dlanorberta@gmail.com", 4);
            ViewBag.mod = taskEdit;

            return View();
        }


        public class dataFromViewCallendar {
            public int id { get; set; }
            
        }
        [HttpPost]
        public ActionResult Home(dataFromViewCallendar data) {

            // Json(string.Empty, JsonRequestBehavior.AllowGet);
            ModelState.Clear();
           
            return RedirectToAction(null);
        }
    }
}