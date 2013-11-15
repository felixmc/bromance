using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class BroRequest
    {
        public int ID { get; set; }
        public User Sender { get; set; }
        public User Receiver { get; set; }
        public string Message { get; set; }
        public DateTime DateSent { get; set; }
    }
}