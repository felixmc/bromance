using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class Product : Entity
    {
        public string  Name { get; set; }
        public decimal Price { get; set; }
        public string[] Tags { get; set; }
        public string Description { get; set; }
        public string Catagory { get; set; }
    }
}