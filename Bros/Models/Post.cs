using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class Post : Entity
    {
        public int MyProperty { get; set; }
        public bool isFlagged { get; set; }
        public Comment[] Comments { get; set; }
    }
}