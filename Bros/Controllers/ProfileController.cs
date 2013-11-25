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
