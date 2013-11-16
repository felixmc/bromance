using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class Photo : Entity
    {
        public Album Album { get; set; }
        public string Caption { get; set; }

    }
}