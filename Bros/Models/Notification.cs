using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class Notification : Entity
    {
        public User Reciever { get; set; }
        public bool isRead { get; set; }
    }
}