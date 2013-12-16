using Bros.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WebMatrix.WebData;

namespace Bros.Controllers
{
    public abstract class BroController : Controller
    {
		protected override void Initialize(RequestContext requestContext)
		{
			ViewBag.IsLoggedIn = WebSecurity.IsAuthenticated;
			ViewBag.CurrentUserId = WebSecurity.CurrentUserId;
			ViewBag.IsAdmin = Roles.IsUserInRole("Admin");

			using (var context = new ModelFirstContainer())
			{
				ViewBag.IsMuted = context.Users.Where(u => u.Id == WebSecurity.CurrentUserId).Select(u => u.IsMuted).FirstOrDefault();
			}

			base.Initialize(requestContext);
		}
    }
}
