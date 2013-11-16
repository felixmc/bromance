using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class User : Entity
    {
        public Profile Brofile { get; set; }
        public int ID { get; set; }
        public List<Circle> CircleList { get; set; }
        public string Email { get; set; }
        public BroRequest[] SentBroRequests { get; set; } //TODO Maybe make these Lists? begin
        public BroRequest[] BroRequestReceived { get; set; }
        public Notification[] Notifications { get; set; }
        public Message[] Messages { get; set; } 
        public Post[] Posts { get; set; }
        public Comment[] Comments { get; set; } // TODO Maybe make these Lists? end

    }
}