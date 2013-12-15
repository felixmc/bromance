using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bros.DataModel;
using Bros.Models;
using WebMatrix.WebData;

namespace Bros.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/
        [Authorize]
        public ActionResult ByUserName()
        {
            String param = Request["searchParam"];
            var context = new ModelFirstContainer();
            var users = context.Users.Where(x => x.Email.Contains(param)).ToList<User>();
            return View("SearchResults",users);
        }

        public ActionResult MatchBros()
        {
            User thisUser = new User();
            List<User> compatibleUsers = new List<User>();
            using(var context = new ModelFirstContainer()){
                thisUser = context.Users.FirstOrDefault(u => u.Id == WebSecurity.CurrentUserId);
            }

            //Dictionary<int, >
            //Matcher match = new Matcher();

            return View("DesireSearch", compatibleUsers);
        }


        public List<User> ByPreferences()
        {
            List<User> compatibleUsers = new List<User>();



            return compatibleUsers;
        }

    }
}
