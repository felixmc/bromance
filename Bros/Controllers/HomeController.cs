using Bros.DataModel;
using Bros.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bros.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        
        public ActionResult Index()
        {

            ViewBag.Title = "Home";
            if ( Session["UserId"] != null && ((int)Session["UserId"]) != 0)
            {
                int id = (int)Session["UserId"];
                User user = new User();
                using(var context = new ModelFirstContainer()){
                    user = context.Users.FirstOrDefault(x => x.Id == id);
                ViewBag.LoginMessage = "Welcome, " + user.Profile.FirstName + "! You are logged in!";
                }
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult AboutUs()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult TermsOfUse()
        {
            return View();
        }
    }
}
