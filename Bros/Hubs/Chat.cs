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
			int senderId = WebSecurity.CurrentUserId;

			using (var context = new ModelFirstContainer())
			{
				User thisUser = context.Users.FirstOrDefault(x => x.Id == senderId);
				Message ms = new Message()
				{
					Content = message,
					DateSent = DateTime.Now,
					Sender = thisUser,
					Receiver = context.Users.Where(u => u.Id == receiverId).FirstOrDefault()
				};
				context.Messages.Add(ms);
				context.SaveChanges();
			}

			if (receiver != null)
			{
				Clients.Clients(receiver.ConnectionIds).receive(senderId, receiverId, message);
			}
			Clients.Clients(sender.ConnectionIds).receive(senderId, receiverId, message);
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