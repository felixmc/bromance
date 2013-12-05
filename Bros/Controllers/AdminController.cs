using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using Bros.DataModel;

namespace Bros.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BanUser()
        {
            
            int id = int.Parse(RouteData.Values["Id"].ToString());
            if(id == 1)
                BanUserById();
            else if(id == 2)
                BanUserByUsername();
            else
                Session["UnbanError"] = "Incorrect Input Parameters";
            return View("Index");
        }

        public ActionResult UnbanUser()
        {
            int id = int.Parse(RouteData.Values["Id"].ToString());
            if (id == 1)
                UnbanUserById();
            else if (id == 2)
                UnbanUserByUsername();
            else
                Session["AdminError"] = "Incorrect Input Parameters";
            return View("Index");
        }

        private void BanUserByUsername()
        {
            Session["AdminError"] = null;
            String username = Request["ban"];
            ModelFirstContainer context = new ModelFirstContainer();
            var userEnumerable = context.Users.Where(x => username == x.Email);
            if (userEnumerable.Count() == 0)
                Session["AdminError"] = "User with " + username + " username does not exist.";
            else
            {
                User user = userEnumerable.First();
                user.IsBanned = true;
                context.SaveChanges();
            }
        }

        private void BanUserById()
        {
            Session["AdminError"] = null;
            int id = 0;
            bool isInteger = int.TryParse(Request["ban"], out id);
            if (isInteger)
            {
                ModelFirstContainer context = new ModelFirstContainer();
                var userEnumberable = context.Users.Where(x => id == x.Id);
                if (userEnumberable.Count() == 0)
                    Session["AdminError"] = "User with " + id + " id does not exist.";
                else
                {
                    User user = userEnumerable.First();
                    user.IsBanned = true;
                    context.SaveChanges();
                }
            }
            else
                Session["AdminError"] = id + " is not a valid id.";
        }

        private void UnbanUserByUsername()
        {
            Session["AdminError"] = null;
        }

        private void UnbanUserById()
        {
            Session["AdminError"] = null;
        }
    }
}
