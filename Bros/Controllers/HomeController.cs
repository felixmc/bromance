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

            ViewBag.ContentClass = "widget wrapper";
			ViewBag.Title = "Welcome";

            return View();
        }

		[Authorize(Roles = "User")]
		[HttpGet]
		public ActionResult Feed(int circleId = 0)
		{
			ViewBag.Title = "Home";

			List<Post> feedPosts = new List<Post>();

			using (ModelFirstContainer context = new ModelFirstContainer())
			{
				int userId = WebSecurity.CurrentUserId;

				User user = context.Users.Where(u => u.Id == userId).FirstOrDefault();

				IEnumerable<Circle> selectedCirlces = circleId == 0 ? user.Circles : user.Circles.Where(c => c.Id == circleId);
				List<int> feedMembers = selectedCirlces.Select(c => c.Members).SelectMany(u => u).Union(context.Users.Where(u => u.Id == userId)).Select(u => u.Id).ToList();

				if (circleId != 0)
					feedMembers.Remove(userId);

				List<Post> broPosts = context.Posts.Include("Comments.Owner.Profile.ProfilePhoto").Include("Author.Profile.ProfilePhoto")
											.Where(p => feedMembers.Contains(p.Author.Id))
													.OrderByDescending(p => p.DateCreated)
															.Take(30).ToList();

				var circles = context.Circles.Where(x => x.Owner.Id == userId).Select(c => new { Id = c.Id, Name = c.Name, Selected = c.Id == circleId }).ToList();
				circles.Add(new { Id = 0, Name = "All", Selected = 0 == circleId });
				ViewBag.Circles = circles;
				feedPosts = broPosts.ToList();
			}

			return View("Feed", feedPosts);
		}

		[Authorize(Roles = "User")]
		[HttpPost]
		public ActionResult Feed()
		{
			int circleId = Int32.Parse(Request["Circle"] ?? "0");
			return Feed(circleId);
		}

        [AllowAnonymous]
        public ActionResult BroCode()
        {
			ViewBag.Title = "Bro Code";
			ViewBag.ContentClass = "widget wrapper";
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
			ViewBag.Title = "About Us";
			ViewBag.ContentClass = "widget wrapper";
            return View();
        }

        [AllowAnonymous]
        public ActionResult Terms()
        {
			ViewBag.Title = "Terms of Use";
			ViewBag.ContentClass = "widget wrapper";
            return View();
        }
    }
}