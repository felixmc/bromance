using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    [Table("TextPost")]
    public class TextPost : Post
    {
        public string Content { get; set; }
    }
}