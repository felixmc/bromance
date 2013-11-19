using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class User : Entity
    {
        public Profile Profile { get; set; }
        public int ID { get; set; }
        public List<Circle> Circles { get; set; }
        public string Email { get; set; }
        public List<BroRequest> SentBroRequests { get; set; }
		public List<BroRequest> ReceivedBroRequest { get; set; }
        public List<Notification> Notifications { get; set; }
        public List<Message> Messages { get; set; } 
        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }
    }
}