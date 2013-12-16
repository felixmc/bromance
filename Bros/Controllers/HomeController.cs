using Bros.DataModel;
using Bros.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Bros.Controllers
{
    public class HomeController : BroController
    {
		[AllowAnonymous]
        public ActionResult Index()
        {
			if (WebSecurity.IsAuthenticated)
				return Feed();

			ViewBag.Title = "Welcome";

            return View();
        }

		[Authorize(Roles = "User")]
		public ActionResult Feed()
		{
			ViewBag.Title = "Home";

			List<Post> feedPosts = new List<Post>();

			using (ModelFirstContainer context = new ModelFirstContainer())
			{
				int userId = WebSecurity.CurrentUserId;

				User user = context.Users.Where(u => u.Id == userId).FirstOrDefault();

				IEnumerable<int> feedMembers = user.Circles.Select(c => c.Members).SelectMany(u => u).Union(context.Users.Where(u => u.Id == userId)).Select(u => u.Id);
				List<Post> broPosts = context.Posts.Include("Comments.Owner.Profile").Include("Author.Profile")
											.Where(p => feedMembers.Contains(p.Author.Id))
													.OrderByDescending(p => p.DateCreated)
															.Take(30).ToList();

				List<Circle> tempList = context.Circles.Where(x => x.Owner.Id == userId).ToList();
				ViewBag.Circles = tempList;
				feedPosts = broPosts.ToList();
			}

			return View("Feed", feedPosts);
		}

		[Authorize(Roles="User")]
		public ActionResult Profile(int? id = null)
		{
			id = id ?? WebSecurity.CurrentUserId;
			ViewBag.IsOwner = id == WebSecurity.CurrentUserId;
			User user = new User();
			using (var context = new ModelFirstContainer())
			{
				user = context.Users.Include("Profile.ProfilePhoto").FirstOrDefault(u => u.Id == id);
			}
			return View(user);
		}

        [AllowAnonymous]
        public ActionResult BroCode()
        {
			ViewBag.Title = "Bro Code";
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
			ViewBag.Title = "About Us";
            return View();
        }

        [AllowAnonymous]
        public ActionResult Terms()
        {
			ViewBag.Title = "Terms of Use";
            return View();
        }
    }
}