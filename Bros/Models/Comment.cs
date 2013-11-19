﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class Comment : Entity
    {
		public virtual User Owner { get; set; }
        public string Content { get; set; }
		public virtual Post ParentPost { get; set; }
        public bool IsFlagged { get; set; }
    }
}