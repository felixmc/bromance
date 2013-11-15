using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class User : Entity
    {
        public Profile Profile { get; set; }
        public int ID { get; set; }
        public List<Circle> CircleList { get; set; }
        public string Email { get; set; }
    }
}