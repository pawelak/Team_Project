using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using TaskMaster.BLL.WebServices;

namespace TaskMaster.Web.Controllers
{
    public class CallendarController : Controller
    {
        private dynamic callendar;
        private bool firsTime = true;

        // GET: Callendar
        public ActionResult Home()
        {
            WebCalService cal = new WebCalService();
            var tmpLog = User.Identity.Name.ToString();
            if (firsTime)
            {
                callendar = cal.Calendar(tmpLog, DateTime.Now.Year, DateTime.Now.Month);
                firsTime = false; 
            }
            ViewBag.mod = callendar;

            return View();
        }

        public class dataFromViewCallendar
        {
            public int month { get; set; }
            public int year { get; set; }
        }
    

        [HttpPost]
        public ViewResult Home(int month, int year)
        {
            var tmpLog = User.Identity.Name.ToString();
            // ModelState.Clear();
            ViewBag.mod = null;
            WebCalService cal = new WebCalService();
            callendar = cal.Calendar(tmpLog, year, month);
            ViewBag.mod = callendar;
           // RedirectToAction("Home");

           return View(); //Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
    }
}