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

				if (WebSecurity.IsAuthenticated)
				{
					IEnumerable<int> friends = context.Users.FirstOrDefault(u => u.Id == WebSecurity.CurrentUserId)
														.Circles.Select(c => c.Members).SelectMany(u => u).Select(u => u.Id);

					ViewBag.Friends = context.Users.Include("Profile").Where(u => friends.Contains(u.Id)).ToList();
					ViewBag.Name = context.Users.Where(u => u.Id == WebSecurity.CurrentUserId).Select(u => u.Profile.FirstName + " " + u.Profile.LastName).FirstOrDefault();
				}
			}

			base.Initialize(requestContext);
		}
	}
}
