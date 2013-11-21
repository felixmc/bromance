using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    [Table("Product")]
    public class Product : Entity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public String ImageFile { get; set; }
        [InverseProperty("Id")]
        [ForeignKey("Tag")]
		public ICollection<String> Tags { get; set; }
        public String Description { get; set; }
        public String Catagory { get; set; }
    }
}