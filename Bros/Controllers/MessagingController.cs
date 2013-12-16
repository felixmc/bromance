using Bros.DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Bros.Controllers
{
	[Authorize(Roles = "User")]
	public class MessagingController : BroController
	{

		public ActionResult Index()
		{
			int id = WebSecurity.CurrentUserId;
			List<Message> allMessages;
			using (var context = new ModelFirstContainer())
			{
				User thisUser = context.Users.FirstOrDefault(x => x.Id == id);
				allMessages = thisUser.ReceivedMessages.ToList();

				allMessages = allMessages.OrderBy(x => x.DateSent).ToList();

				List<User> uniqueUsers = context.Users.Include("Profile").ToList();

				ViewBag.messages = allMessages;

				IEnumerable<int> messages = thisUser.SentMessages.Union(thisUser.ReceivedMessages).Select(m => m.Id);
				List<User> userList = context.Users.Include("Profile").Where(u => u.Id != thisUser.Id).Where(u => u.SentMessages.Union(u.ReceivedMessages).Select(m => m.Id).Intersect(messages).Count() != 0).Distinct().ToList();

				ViewBag.UserConversations = userList;
			}

			ViewBag.Title = "Messages";
			ViewBag.ContentClass = "condensed";

			return View(allMessages);
		}

		public ActionResult Conversation(int id)
		{
			int userId = WebSecurity.CurrentUserId;
			List<Message> allMessages = new List<Message>();
			using (var context = new ModelFirstContainer())
			{
				User messenger = context.Users.FirstOrDefault(u => u.Id == id);
				User thisUser = context.Users.FirstOrDefault(x => x.Id == userId);
				allMessages = context.Messages.Include("Sender.Profile").Include("Receiver.Profile").Where(m => (m.Sender.Id == id && m.Receiver.Id == thisUser.Id)
																											|| (m.Sender.Id == thisUser.Id && m.Receiver.Id == id)).OrderBy(m => m.DateSent).Take(30).ToList();
				ViewBag.ReceiverId = id;
				ViewBag.Title = String.Format("{0} {1}", messenger.Profile.FirstName, messenger.Profile.LastName);
			}

			ViewBag.ContentClass = "condensed";
			return View(allMessages);
		}

		[HttpGet]
		public ActionResult CreateMessage()
		{
			using (var context = new ModelFirstContainer())
			{
				int id = WebSecurity.CurrentUserId;
				User user = context.Users.Include("Profile").FirstOrDefault(x => x.Id == id);

				IEnumerable<User> users = context.Circles.Include("Members.Profile.ProfilePhoto").Where(x => x.Owner.Id == user.Id).Select(c => c.Members).SelectMany(u => u).Distinct();

				foreach (User u in users)
				{
					context.Entry(u.Profile).Reference(p => p.ProfilePhoto).Load();
				}

				ViewBag.Users = users.ToList();
				ViewBag.Title = "Create Message";
			}

			ViewBag.ContentClass = "condensed";
			return View();
		}

		[HttpPost]
		public ActionResult CreateMessage(Message message)
		{
			int id = WebSecurity.CurrentUserId;
			int receiverId = Int32.Parse(Request["Receiver"]);
			using (var context = new ModelFirstContainer())
			{
				User thisUser = context.Users.FirstOrDefault(x => x.Id == id);
				Message ms = new Message()
				{
					Content = message.Content,
					DateSent = DateTime.Now,
					Sender = thisUser,
					Receiver = context.Users.Where(u => u.Id == receiverId).FirstOrDefault()
				};
				context.Messages.Add(ms);
				context.SaveChanges();
			}

			return new RedirectResult(Request.UrlReferrer.AbsolutePath);
		}

		public ActionResult Chat()
		{
			using (var context = new ModelFirstContainer())
			{
				IEnumerable<int> friends = context.Users.FirstOrDefault(u => u.Id == WebSecurity.CurrentUserId)
													.Circles.Select(c => c.Members).SelectMany(u => u).Select(u => u.Id);

				ViewBag.Friends = context.Users.Include("Profile").Where(u => friends.Contains(u.Id)).ToList();
				ViewBag.Name = context.Users.Where(u => u.Id == WebSecurity.CurrentUserId).Select(u => u.Profile.FirstName + " " + u.Profile.LastName).FirstOrDefault();
			}

			ViewBag.UserId = WebSecurity.CurrentUserId;

			return View();
		}

	}
}