using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class Comment : Entity
    {
        public User CommentCreator { get; set; }
        public string CommentBody { get; set; }
        public bool isDeleted { get; set; }
        public Post CommentParent { get; set; }
        public bool IsFlagged { get; set; }
    }
}