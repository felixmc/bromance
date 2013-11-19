using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class Post : Entity
    {
		public virtual User Owner { get; set; }
        public bool IsFlagged { get; set; }
		public virtual Comment[] Comments { get; set; }
    }
}