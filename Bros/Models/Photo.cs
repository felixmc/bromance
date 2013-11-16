using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class Photo : Entity
    {
<<<<<<< HEAD
        public int ID { get; set; }
        public bool IsDeleted { get; set; }
=======
        public Album Album { get; set; }
        public string Caption { get; set; }
>>>>>>> ef27a5ab59cf7619a3cf11f1b876b4c62cda350c

    }
}