using Bros.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Bros.Controllers
{
	[Authorize(Roles="User")]
    public class ContentController : BroController
    {

		[HttpPost]
		public ActionResult PostStatus()
		{
			using (ModelFirstContainer context = new ModelFirstContainer())
			{
				int userId = WebSecurity.CurrentUserId;
				User user = context.Users.Where(u => u.Id == userId).FirstOrDefault();

				TextPost update = new TextPost() { Author = user, Content = Request["status"], DateCreated = DateTime.Now, DateUpdated = DateTime.Now };
				user.Posts.Add(update);

				context.SaveChanges();
			}

			if (Request.IsAjaxRequest())
				return null;
			else
				return Redirect(Request.UrlReferrer.AbsolutePath);
		}

		public ActionResult Post(int id)
		{
			return View();
		}

		[HttpPost]
		public ActionResult PostComment()
		{
			using (ModelFirstContainer context = new ModelFirstContainer())
			{
				int postId = Int32.Parse(Request["postId"]);
				Post post = context.Posts.Where(p => p.Id == postId).FirstOrDefault();

				if (post != null)
				{
					int userId = WebSecurity.CurrentUserId;
					User user = context.Users.FirstOrDefault(u => u.Id == userId);
					Comment comment = new Comment() { Content = Request["comment"], Owner = user, ParentPost = post, DateCreated = DateTime.Now };

					CommentNotification not = new CommentNotification() { Comment = comment, DateCreated = DateTime.Now, IsRead = false, Receiver = post.Author };

					comment.CommentNotifications.Add(not);
					user.Comments.Add(comment);
					post.Comments.Add(comment);

					context.SaveChanges();
				}

			}

			if (Request.IsAjaxRequest())
				return null;
			else
				return Redirect(Request.UrlReferrer.AbsolutePath);
		}

    }
}