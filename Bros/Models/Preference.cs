using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class Preference : Entity
    {
		public String Key { get; set; }
		public String Value { get; set; }
    }
}