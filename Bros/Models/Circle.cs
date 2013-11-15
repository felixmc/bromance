using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class Circle
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<User> UserList { get; set; }

    }
}