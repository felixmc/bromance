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
        [InverseProperty("Id")]
        [ForeignKey("User")]
		public virtual User Owner { get; set; }
		public String Key { get; set; }
		public String Value { get; set; }
    }
}