using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class Entity
    {
        public int ID { get; set; }
        public DateTime DateCreated { get; set; }
<<<<<<< HEAD
        public DateTime DateModified { get; set; }
=======
        public DateTime LastDateModified { get; set; }
        public bool isDeleated { get; set; }

>>>>>>> ef27a5ab59cf7619a3cf11f1b876b4c62cda350c
    }
}