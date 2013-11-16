using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class Post : Entity
    {
<<<<<<< HEAD
        public int ID { get; set; }
        public int MyProperty { get; set; }
=======
        public bool isFlagged { get; set; }
        public Comment Comments { get; set; }
>>>>>>> ef27a5ab59cf7619a3cf11f1b876b4c62cda350c
    }
}