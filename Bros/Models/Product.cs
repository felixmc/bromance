﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public String ImageFile { get; set; }
        public List<String> Tags { get; set; }
        public String Description { get; set; }
        public String Catagory { get; set; }
    }
}