using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class Notification : Entity
    {
        public User Receiver { get; set; }
        public bool IsRead { get; set; }
    }
}