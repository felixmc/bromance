using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class Product : Entity
    {
<<<<<<< HEAD
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public List<string> Tags { get; set; }
        public string Description { get; set; }

=======
        public string  Name { get; set; }
        public decimal Price { get; set; }
        public string[] Tags { get; set; }
        public string Description { get; set; }
        public string Catagory { get; set; }
>>>>>>> ef27a5ab59cf7619a3cf11f1b876b4c62cda350c
    }
}