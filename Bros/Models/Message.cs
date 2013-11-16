using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class Message : Entity
    {
        public User Sender { get; set; }
        public User Reciever { get; set; }
        public string  Message { get; set; }
        public DateTime SeenOn { get; set; }
    }
}