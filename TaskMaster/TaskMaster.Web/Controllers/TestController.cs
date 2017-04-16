using System.Web.Mvc;
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