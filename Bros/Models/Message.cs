using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class Message : Entity
    {
		public virtual User Sender { get; set; }
		public virtual User Receiver { get; set; }
        public string Content { get; set; }
        public DateTime DateSeen { get; set; }
    }
}