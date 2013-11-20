using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    [Table("Hobby")]
    public class Hobby : Entity
    {
		public String Name { get; set; }
    }
}