using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using TaskMaster.Web.Models;

namespace TaskMaster.Web.Controllers
{
    public class HomePageController : Controller
    {
        // GET: HomePage
        public ViewResult Index()
        {

            var userId = User.Identity.GetUserId();

            if (userId != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userName = User.Identity.GetUserName();

                    return View();
                }

            }
            return View("View");
        }
    }
}