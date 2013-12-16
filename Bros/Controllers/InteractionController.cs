using Bros.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Bros.Controllers
{
	[Authorize(Roles = "User")]
    public class InteractionController : BroController
    {
		public ActionResult Notifications()
		{
			List<Notification> notifications = new List<Notification>();
			using (ModelFirstContainer context = new ModelFirstContainer())
			{
				int userId = WebSecurity.CurrentUserId;

				foreach (Notification not in context.Notifications.Where(n => n.Receiver.Id == userId && n.IsRead == false))
				{
					if (not is CommentNotification)
					{
						CommentNotification cn = not as CommentNotification;
						context.Entry(cn.Comment.Owner).Reference(u => u.Profile).Load();
						notifications.Add(cn);
					}
					else if (not is Bros.DataModel.RequestNotification)
					{
						Bros.DataModel.RequestNotification cn = not as Bros.DataModel.RequestNotification;
						context.Entry(cn.BroRequest.Sender).Reference(u => u.Profile).Load();
						notifications.Add(cn);
					}
				}
			}

			ViewBag.Title = "Notifications";

			return View(notifications);
		}

		[HttpPost]
		public ActionResult ReadNotification(int id)
		{
			using (ModelFirstContainer context = new ModelFirstContainer())
			{
				Notification not = context.Notifications.Where(n => n.Id == id).FirstOrDefault();

				if (not != null)
				{
					not.IsRead = true;
					context.SaveChanges();
				}
			}

			if (Request.IsAjaxRequest())
				return null;
			else
				return Redirect(Request.UrlReferrer.AbsolutePath);
		}


		public ActionResult SendBroRequest(int id)
		{
			using (ModelFirstContainer context = new ModelFirstContainer())
			{
				int senderId = WebSecurity.CurrentUserId;
				User user = context.Users.FirstOrDefault(u => u.Id == senderId);

				User reciever = context.Users.FirstOrDefault(u => u.Id == id);

				BroRequest request = CreateRequest(user, reciever);

				user.SentBroRequests.Add(request);
				reciever.ReceivedBroRequests.Add(request);

				context.SaveChanges();
			}

			return new RedirectResult(Request.UrlReferrer.AbsolutePath);
		}

		public BroRequest CreateRequest(User Sender, User Receiver)
		{
			BroRequest request = new BroRequest()
			{
				DateCreated = DateTime.Now,
				Sender = Sender,
				Receiver = Receiver,
				Message = ""
			};

			request.RequestNotification = new Bros.DataModel.RequestNotification()
			{
				BroRequest = request,
				DateCreated = DateTime.Now,
				Receiver = Receiver,
				IsRead = false
			};

			return request;
		}

		public ActionResult BroAccept(int id)
		{

			using (ModelFirstContainer context = new ModelFirstContainer())
			{
				BroRequest request = context.BroRequests.Include("Sender.Profile").Include("Receiver.Profile").FirstOrDefault(r => r.Id == id);

				context.Circles.FirstOrDefault(c => c.Name.Equals("MyBros") && c.Owner.Id == request.Sender.Id).Members.Add(request.Receiver);
				context.Circles.FirstOrDefault(c => c.Name.Equals("MyBros") && c.Owner.Id == request.Receiver.Id).Members.Add(request.Sender);

				request.RequestNotification.IsRead = true;

				int userId = WebSecurity.CurrentUserId;
				User user = context.Users.Include("Circles").Include("Circles.Members.Profile").FirstOrDefault(u => u.Id == userId);
				ViewBag.Bros = context.Circles.FirstOrDefault(c => c.Name.Equals("MyBros")).Members.Where(u => u.Id != user.Id).ToList();
				ViewBag.Request = request;

				context.SaveChanges();
			}

			return new RedirectResult(Request.UrlReferrer.AbsolutePath);
		}

		public ActionResult DismissRequest(int id)
		{
			using (ModelFirstContainer context = new ModelFirstContainer())
			{
				context.BroRequests.SingleOrDefault(x => x.Id == id).RequestNotification.IsRead = true;
				context.SaveChanges();
			}

			return new RedirectResult(Request.UrlReferrer.AbsolutePath);
		}

		public ActionResult BlockBro(int id)
		{

			using (var context = new ModelFirstContainer())
			{
				int userId = WebSecurity.CurrentUserId;
				User blockedUser = context.Users.FirstOrDefault(u => u.Id == id);
				User thisUser = context.Users.FirstOrDefault(u => u.Id == userId);
				thisUser.BlockedBros.Add(blockedUser);

				IEnumerable<Circle> circleList = context.Circles.Where(c => c.Owner.Id == thisUser.Id);
				foreach (Circle circle in circleList)
				{
					circle.Members.Remove(blockedUser);
				}
				context.SaveChanges();

			}
			return RedirectToAction("ViewBros", "Profile");
		}

		public ActionResult UnblockBro(int id)
		{
			User blockedUser = new User();
			User thisUser = new User();
			using (var context = new ModelFirstContainer())
			{
				blockedUser = context.Users.Include("BlockedBros.Profile").FirstOrDefault(u => u.Id == id);
				thisUser = context.Users.Include("BlockedBros.Profile").FirstOrDefault(u => u.Id == WebSecurity.CurrentUserId);
				thisUser.BlockedBros.Remove(blockedUser);
				context.SaveChanges();
				ViewBag.Success = "You have unblocked " + blockedUser.Profile.FirstName + " " + blockedUser.Profile.LastName;
			}

			return View("Settings", thisUser);
		}

		public void RemoveBro(int targetBroId)
		{
			using (ModelFirstContainer context = new ModelFirstContainer())
			{
				int userId = WebSecurity.CurrentUserId;
				User user = context.Users.SingleOrDefault(x => x.Id == userId);
				User targetUser = context.Users.SingleOrDefault(x => x.Id == targetBroId);

				foreach (Circle c in context.Circles.Where(c => c.Owner.Id == userId))
				{
					c.Members.Remove(targetUser);
				}

				foreach (Circle c in context.Circles.Where(c => c.Owner.Id == targetBroId))
				{
					c.Members.Remove(user);
				}

				context.SaveChanges();
			}
		}

		[HttpPost]
		public ActionResult Bump(int id)
		{
			using (ModelFirstContainer context = new ModelFirstContainer())
			{
				int userId = WebSecurity.CurrentUserId;

				// get last bump between the current user and the specified user
				FirstBump lastBump = context.Notifications.Where(n => (n.Receiver.Id == id || n.Receiver.Id == userId) && n is FirstBump)
																.Select(n => n as FirstBump)
																	.Where(b => b.Sender.Id == userId || b.Sender.Id == id)
																		.OrderByDescending(b => b.DateCreated).FirstOrDefault();

				// if the last bump was sent by the user that I'm bumping or there has been no last bump
				if (lastBump == null && lastBump.Sender.Id == id)
				{
					User bumper = context.Users.Where(u => u.Id == userId).FirstOrDefault();
					User bumpee = context.Users.Where(u => u.Id == id).FirstOrDefault();

					FirstBump fistBump = new FirstBump() { DateCreated = DateTime.Now, Receiver = bumpee, Sender = bumper };

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