using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bros.DataModel;

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

    }
}
