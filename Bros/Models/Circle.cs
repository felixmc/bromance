using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class Circle : Entity
    {
        public string Name { get; set; }
        public List<User> UserList { get; set; }

    }
}