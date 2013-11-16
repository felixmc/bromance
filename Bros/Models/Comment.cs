using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class Comment : Entity
    {
        public User Creator { get; set; }
        public string CommentBody { get; set; }
        public Post CommentedOnPost { get; set; }
        public bool isFlagged { get; set; }
    }
}