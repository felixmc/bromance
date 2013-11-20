using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    [Table("Circle")]
    public class Circle : Entity
    {
        public string Name { get; set; }
		public virtual ICollection<User> UserList { get; set; }
		public virtual User Owner { get; set; }
    }
}