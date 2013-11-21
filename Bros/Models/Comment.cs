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
        [InverseProperty("ID")]
        [ForeignKey("User")]
		public virtual User Owner { get; set; }
        public string Content { get; set; }
        [InverseProperty("ID")]
        [ForeignKey("Parent")]
		public virtual Post ParentPost { get; set; }
        public bool IsFlagged { get; set; }
    }
}