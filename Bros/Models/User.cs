using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class User : Entity
    {
		public virtual Profile Profile { get; set; }
        public int ID { get; set; }
		public virtual List<Circle> Circles { get; set; }
        public string Email { get; set; }
		public virtual List<BroRequest> SentBroRequests { get; set; }
		public virtual List<BroRequest> ReceivedBroRequest { get; set; }
		public virtual List<Notification> Notifications { get; set; }
		public virtual List<Message> Messages { get; set; }
		public virtual List<Post> Posts { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}