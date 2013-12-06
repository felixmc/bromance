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
            string id = Request["id"].ToString();
            if (Request["id"].ToString().Equals("1"))
                BanUserById();
            else if (Request["id"].ToString().Equals("2"))
                BanUserByUsername();
            else
                Session["UnbanError"] = "Incorrect Input Parameters";
            return View("Index");
        }

        public ActionResult UnbanUser()
        {
            int id = int.Parse(Request["id"].ToString());
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
                    User user = userEnumberable.First();
                    user.IsBanned = true;
                    context.SaveChanges();
                }
            }
            else
                Session["AdminError"] = Request["ban"] + " is not a valid id.";
        }

        private void UnbanUserByUsername()
        {
            Session["AdminError"] = null;
            String username = Request["unban"];
            ModelFirstContainer context = new ModelFirstContainer();
            var userEnumerable = context.Users.Where(x => username == x.Email);
            if (userEnumerable.Count() == 0)
                Session["AdminError"] = "User with " + username + " username does not exist.";
            else
            {
                User user = userEnumerable.First();
                if (user.IsBanned)
                {
                    user.IsBanned = false;
                    context.SaveChanges();
                }
                else
                {
                    Session["AdminError"] = "User " + user.Email + " was not banned.";
                }
            }
        }

        private void UnbanUserById()
        {
            Session["AdminError"] = null;
            int id = 0;
            bool isInteger = int.TryParse(Request["unban"], out id);
            if (isInteger)
            {
                ModelFirstContainer context = new ModelFirstContainer();
                var userEnumberable = context.Users.Where(x => id == x.Id);
                if (userEnumberable.Count() == 0)
                    Session["AdminError"] = "User with " + id + " id does not exist.";
                else
                {
                    User user = userEnumberable.First();
                    if (user.IsBanned)
                    {
                        user.IsBanned = false;
                        context.SaveChanges();
                    }
                    else
                    {
                        Session["AdminError"] = "User " + user.Email + " was not banned.";
                    }
                }
            }
            else
                Session["AdminError"] = Request["ban"] + " is not a valid id.";
        }
    }
}
