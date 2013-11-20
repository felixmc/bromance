using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class Album : Entity
    {
		public virtual User Owner { get; set; }
        public string Title { get; set; }
		public virtual ICollection<Photo> Photos { get; set; }
    }
}