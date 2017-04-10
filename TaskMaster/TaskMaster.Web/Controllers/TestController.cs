using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TaskMaster.DAL.DTOModels;
using TaskMaster.BLL.Services;

namespace TaskMaster.Web.Controllers
{
    public class TestController : Controller
    {
        private readonly TestService _testService = new TestService();
        // GET: Test
        public ActionResult Index()
        {

           ViewBag.MyList = _testService.GetAllInList();
            

            return View();
        }

        public  ViewResult Cos()
        {
            return View(_testService.GetAllInList());
        }
    }
}