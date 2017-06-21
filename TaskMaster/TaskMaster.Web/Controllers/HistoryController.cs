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
            var tmpLog = User.Identity.Name.ToString();

            ViewBag.mod = hist.History(tmpLog);

          //  y = new List<string> {(TempData["Data1"]).ToString()};

            return View();
        }


        public ActionResult Edit()
        {
            WebHistEditService _webHistEditService = new WebHistEditService();
            var tmpLog = User.Identity.Name.ToString();

            var taskEdit = _webHistEditService.ShowEditTask(tmpLog, 4);
            ViewBag.mod = taskEdit;

            return View();
        }


        public class dataFromViewCallendar {
            public int id { get; set; }
            
        }
        [HttpPost]
        public ActionResult Home(dataFromViewCallendar data) {

            // Json(string.Empty, JsonRequestBehavior.AllowGet);
           
           
            return RedirectToAction("Edit","History");

           // var redirectUrl = new UrlHelper(Request.RequestContext).Action("Edit","History");
           // return Json(new {Url = redirectUrl});
        }
    }
}