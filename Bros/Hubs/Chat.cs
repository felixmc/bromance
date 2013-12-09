using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using WebMatrix.WebData;
using Bros.DataModel;
using System.Threading.Tasks;

namespace Bros.Hubs
{
	[HubName("chatHub")]
	public class Chat : Hub
	{
		public static List<ChatUser> onlineUsers = new List<ChatUser>();

		public void Send(int receiverId, string message)
		{
			ChatUser receiver = onlineUsers.FirstOrDefault(u => u.UserId == receiverId);
			ChatUser sender = onlineUsers.FirstOrDefault(u => u.UserId == WebSecurity.CurrentUserId);

			//using (var context = new ModelFirstContainer())
			//{
			//	int id = (int)Session["UserId"];
			//	int receiverId = Int32.Parse(Request["Receiver"]);
			//	using (var context = new ModelFirstContainer())
			//	{
			//		User thisUser = context.Users.FirstOrDefault(x => x.Id == id);
			//		Message ms = new Message()
			//		{
			//			Content = message.Content,
			//			DateSent = DateTime.Now,
			//			DateRead = DateTime.Now,
			//			Sender = thisUser,
			//			Receiver = context.Users.Where(u => u.Id == receiverId).FirstOrDefault()
			//		};
			//		context.Messages.Add(ms);
			//		context.SaveChanges();

			//		IEnumerable<int> messages = thisUser.SentMessages.Union(thisUser.ReceivedMessages).Select(m => m.Id);
			//		List<User> userList = context.Users.Include("Profile").Where(u => u.SentMessages.Union(u.ReceivedMessages).Select(m => m.Id).Intersect(messages).Count() != 0).ToList();

			//		ViewBag.UserConversations = userList;
			//	}
			//}

			if (receiver != null)
			{
				Clients.Clients(receiver.ConnectionIds).receive(WebSecurity.CurrentUserId, receiverId, message);
			}
			Clients.Clients(sender.ConnectionIds).receive(WebSecurity.CurrentUserId, receiverId, message);
		}

		public void Authenticate()
		{
			ChatUser user = onlineUsers.FirstOrDefault(u => u.UserId == WebSecurity.CurrentUserId);
			bool isOnline = user != null;
			if (!isOnline)
			{
				user = new ChatUser() { UserId = WebSecurity.CurrentUserId };
				using (var context = new ModelFirstContainer())
				{
					user.Friends = context.Users.FirstOrDefault(u => u.Id == WebSecurity.CurrentUserId)
											.Circles.Select(c => c.Members)
														.SelectMany(u => u).Select(u => u.Id).Distinct().ToList();
				}
			}

			user.ConnectionIds.Add(Context.ConnectionId);

			List<int> onlineFriends = new List<int>();
			foreach (int friendId in user.Friends)
			{
				ChatUser friend = onlineUsers.FirstOrDefault(u => u.UserId == friendId);

				if (friend != null)
				{
					if (!isOnline) Clients.Clients(friend.ConnectionIds).friendOnline(WebSecurity.CurrentUserId);
					onlineFriends.Add(friend.UserId);
				}
			}

			Clients.Client(Context.ConnectionId).friendList(onlineFriends);

			
			onlineUsers.Add(user);
		}

		public override Task OnDisconnected()
		{
			ChatUser user = onlineUsers.FirstOrDefault(u => u.ConnectionIds.Contains(Context.ConnectionId));
			if (user != null)
			{
				user.ConnectionIds.Remove(Context.ConnectionId);
				if (user.ConnectionIds.Count == 0)
				{
					foreach (int friendId in user.Friends)
					{
						ChatUser friend = onlineUsers.FirstOrDefault(u => u.UserId == friendId);

						if (friend != null)
						{
							Clients.Clients(friend.ConnectionIds).friendOffline(user.UserId);
						}
					}

					onlineUsers.Remove(user);
				}
			}

			return base.OnDisconnected();
		}
	}
}