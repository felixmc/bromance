using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class Message : Entity
    {
        public User Sender { get; set; }
<<<<<<< HEAD
        public User Receiver { get; set; }
        public string Message { get; set; }
        public DateTime Seen { get; set; }
=======
        public User Reciever { get; set; }
        public string  Message { get; set; }
        public DateTime SeenOn { get; set; }
>>>>>>> ef27a5ab59cf7619a3cf11f1b876b4c62cda350c
    }
}