using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    [Table("Photo")]
    public class Photo : Post
    {
		public virtual Album Album { get; set; }
        public String Caption { get; set; }
    }
}