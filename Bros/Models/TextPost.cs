using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class TextPost :Post, Entity
    {
        public string Content { get; set; }
    }
}