using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class BroRequest : Entity
    {
		public virtual User Sender { get; set; }
		public virtual User Receiver { get; set; }
        public string Message { get; set; }
    }
}