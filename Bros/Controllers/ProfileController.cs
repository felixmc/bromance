using Bros.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bros.Controllers
{
    public class ProfileController : Controller
    {
        //
        // GET: /Profile/

        public ActionResult Index()
        {
            return View();
            
        }

		public ActionResult Feed()
		{
			return View();
		}

		public new ActionResult Profile()
		{
			return View();
		}

        public ActionResult BroAccept(BroRequest request)
        {

            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                request.Accept();
                context.SaveChanges();

                User user = (User)Session["User"];
                ViewBag.Bros = user.GetCircleByName("Bros").Members;
                ViewBag.Request = request;
            }

            return View();
        }

		[HttpPost]
		public ActionResult PostStatus()
		{
			using (ModelFirstContainer context = new ModelFirstContainer())
			{
				User user = (User) Session["User"];

				TextPost update = new TextPost() { Author = user, Content = Request["status"], DateCreated = new DateTime() };
				user.Posts.Add(update);

				context.SaveChanges();
			}

			if (Request.IsAjaxRequest())
				return null;
			else
				return Redirect( Request.UrlReferrer.AbsolutePath );
		}

    }
}
