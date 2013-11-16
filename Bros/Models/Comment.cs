using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class Comment : Entity
    {
<<<<<<< HEAD
        public User CommentCreator { get; set; }
        public string CommentBody { get; set; }
        public bool isDeleted { get; set; }
        public Post CommentParent { get; set; }
        public bool IsFlagged { get; set; }
=======
        public User Creator { get; set; }
        public string CommentBody { get; set; }
        public Post CommentedOnPost { get; set; }
        public bool isFlagged { get; set; }
>>>>>>> ef27a5ab59cf7619a3cf11f1b876b4c62cda350c
    }
}