using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
<<<<<<< HEAD
    public class Album : Entity
    {
        public User Owner { get; set; }
        public string Name { get; set; }
=======
    public class Album : Post
    {
        public User Owner { get; set; }
        public string Title { get; set; }
        public Photo[] Photos { get; set; }
>>>>>>> ef27a5ab59cf7619a3cf11f1b876b4c62cda350c
    }
}