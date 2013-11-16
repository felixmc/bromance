using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class Photo : Entity
    {
        public int ID { get; set; }
        public bool IsDeleted { get; set; }

    }
}