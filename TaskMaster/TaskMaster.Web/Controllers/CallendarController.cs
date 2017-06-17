using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskMaster.BLL.WebServices;

namespace TaskMaster.Web.Controllers
{
    public class CallendarController : Controller
    {
        // GET: Callendar
        public ActionResult Home()
        {
            WebCalService cal = new WebCalService();
            var callendar = cal.Calendar("dlanorberta@gmail.com", DateTime.Now.Year, DateTime.Now.Month);
            ViewBag.mod = callendar;

            return View();
        }

        public class dataFromViewCallendar
        {
            public int month { get; set; }
            public int year { get; set; }
        }
    

        [HttpPost]
        public ActionResult Home(int month, int year)
        {

            WebCalService cal = new WebCalService();
            var callendar = cal.Calendar("dlanorberta@gmail.com", year, month);
            ViewBag.mod = callendar;

           // return Json(string.Empty, JsonRequestBehavior.AllowGet);
            return View();
        }
    }
}