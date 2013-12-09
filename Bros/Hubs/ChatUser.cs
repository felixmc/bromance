using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Hubs
{
	public class ChatUser
	{
		public int UserId { get; set; }
		public List<int> Friends { get; set; }
		public List<string> ConnectionIds { get; private set; }
		
		public ChatUser()
		{
			ConnectionIds = new List<string>();
		}

		public override bool Equals(System.Object obj)
		{
			if (obj == null) return false;

			ChatUser u = obj as ChatUser;
			if ((System.Object)u == null) return false;

			return UserId == u.UserId;
		}

	}
}