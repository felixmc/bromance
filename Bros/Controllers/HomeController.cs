using Bros.DataModel;
using Bros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bros.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {

            List<User> userList = new List<User>();
            using(var context = new BroContext()){

                User user = new User()
                {
                    Email = "AwesomePossume@gmail.com"
                };

                context.Users.Add(user);
                context.SaveChanges();
                userList.Add(user);
            }
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }
    }
}
