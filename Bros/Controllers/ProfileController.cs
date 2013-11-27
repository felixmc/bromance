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

        public ActionResult SendBroRequest(User reciever)
        {
            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                User user = (User)Session["User"];
            }

            return View();
        }

		public ActionResult Feed()
		{
			List<Post> feedPosts = new List<Post>();

			using (ModelFirstContainer context = new ModelFirstContainer())
			{
				User user = (User)Session["User"];
				ICollection<Post> broPosts = user.Circles.Select(c => c.Members).SelectMany(u => u)
												.Select(u => u.Posts).SelectMany(p => p)
													.OrderByDescending(p => p.DateCreated)
														.Take(30).ToList();
				feedPosts = broPosts.ToList();
			}

			return View(feedPosts);
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

		public new ActionResult Profile()
		{
			return View();
		}

		[HttpPost]
		public ActionResult UpdateProfile(Profile profile)
		{
			return null;
		}

		[HttpPost]
		public ActionResult PostStatus()
		{
			using (ModelFirstContainer context = new ModelFirstContainer())
			{
				User user = (User)Session["User"];

				TextPost update = new TextPost() { Author = user, Content = Request["status"], DateCreated = new DateTime() };
				user.Posts.Add(update);

				context.SaveChanges();
			}

			if (Request.IsAjaxRequest())
				return null;
			else
				return Redirect(Request.UrlReferrer.AbsolutePath);
		}

		[HttpPost]
		public ActionResult PostComment()
		{
			using (ModelFirstContainer context = new ModelFirstContainer())
			{
				int postId = Int32.Parse(Request["post"]);

				Post post = context.Posts.Where(p => p.Id == postId).FirstOrDefault(null);

				if (post != null)
				{
					User user = (User)Session["User"];
					Comment comment = new Comment() { Content = Request["comment"], Owner = user, ParentPost = post, DateCreated = new DateTime() };

					user.Comments.Add(comment);

					context.SaveChanges();
				}

			}

			if (Request.IsAjaxRequest())
				return null;
			else
				return Redirect(Request.UrlReferrer.AbsolutePath);
		}

        [HttpPost]
        public ActionResult SetPreference(Preference pref)
        {

            if(ModelState.IsValid)
            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                

            }

            return View("Index");
        }

	}
}