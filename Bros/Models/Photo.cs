using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class Photo : Post
    {
		public virtual Album Album { get; set; }
        public String Caption { get; set; }
    }
}