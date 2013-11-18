using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class Message : Entity
    {
        public User Sender { get; set; }
        public User Receiver { get; set; }
        public string Content { get; set; }
        public DateTime Seen { get; set; }
    }
}