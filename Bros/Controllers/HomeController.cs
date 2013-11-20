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

        public ActionResult Index()
        {

            List<TextPost> userList = new List<TextPost>();
            using(var context = new BroContext()){

                TextPost tp = new TextPost { Content = "test" };
                context.Entities.Add(tp);
                try
                {
                    context.SaveChanges();
                }
                catch(DbEntityValidationException e){
                    foreach (var validationErrors in e.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
                userList.Add(tp);
            }
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }
    }
}
