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
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        ProfileFactory fact = new ProfileFactory();
        public ActionResult Index()
        {

            List<User> userList = new List<User>();
            using (var context = new ModelFirstContainer())
            {

				//User user = fact.User;
				//context.Users.Add(user);
				//try
				//{
				//	context.SaveChanges();
				//}
				//catch (DbEntityValidationException e)
				//{
				//	foreach (var validationErrors in e.EntityValidationErrors)
				//	{
				//		foreach (var validationError in validationErrors.ValidationErrors)
				//		{
				//			Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
				//		}
				//	}
				//}
				//userList.Add(user);

				context.Tags.Add(new Tag() { Name = "MyTag" });
				context.SaveChanges();

            }
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }
    }
}
