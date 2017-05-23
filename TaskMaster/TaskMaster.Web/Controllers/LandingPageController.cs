using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Microsoft.Ajax.Utilities;
using Microsoft.Owin.Security;
using TaskMaster.Web.Models;
using TaskMaster.BLL.WebServices;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace TaskMaster.Web.Controllers
{
    public class LandingPageController : Controller
    {
        private readonly WebUserService _webTestService = new WebUserService();

        // GET: LandingPage
        public ActionResult Home()
        {   
           //ViewBag.Message = _webTestService.GetUser().Name;
           return View();
        }

        [HttpPost]
        public ActionResult Home(UserAccount user)
        {
            using (UsersDataBase db = new UsersDataBase())
            {
                var usr = db.UserAccounts.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
                if (usr != null)
                {
                    Session["UserId"] = usr.UserId.ToString();
                    Session["Email"] = usr.Email.ToString();
                    
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "Email lub hasło jest niepoprawne.");
                }
            }

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserAccount account)
        {
            if (ModelState.IsValid)
            {
                using (UsersDataBase db = new UsersDataBase())
                {
                    db.UserAccounts.Add(account);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = account.FirstName + " " + account.LastName + " poprawnie zarejstrowany."; //dodać opoxnienie i przekierowanie
            }
            return View();
        }


        public ActionResult LoggedIn()
        {
            if ( 1 == 1 )
            {
                return View();
            }
            else
            {
                return RedirectToAction("Home");
            }
        }

        //----------------------------------------------------Do logowania--------------------------------------------------------------------------------------
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult ExternalLogin(string provider, string returnUrl)
        //{
        //    // Request a redirect to the external login provider
        //    return new AccountController.ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        //}


    }
}