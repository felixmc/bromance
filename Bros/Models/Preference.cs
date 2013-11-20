using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    [Table("Preference")]
    public class Preference : Entity
    {
		public User Owner { get; set; }
		public String Key { get; set; }
		public String Value { get; set; }
    }
}