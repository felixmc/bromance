using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bros.DataModel;

namespace Bros.Models
{
    public class Compatibility
    {
        public User User { get; set; }
        public int CompatibilityOfUser { get; set; }

        public void Add(User user, int compatability)
        {
            this.User = user;
            this.CompatibilityOfUser = compatability;
        }
    }
}