using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    [Table("Comment")]
    public class Comment : Entity
    {
        [InverseProperty("Id")]
        [ForeignKey("User")]
		public virtual User Owner { get; set; }
        public string Content { get; set; }
        [InverseProperty("Id")]
        [ForeignKey("Parent")]
		public virtual Post ParentPost { get; set; }
        public bool IsFlagged { get; set; }
    }
}