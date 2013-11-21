using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    [Table("Post")]
    public abstract class Post : Entity
    {
        [InverseProperty("ID")]
        [ForeignKey("User")]
		public virtual User Owner { get; set; }
        public bool IsFlagged { get; set; }
        [InverseProperty("ID")]
        [ForeignKey("Comment")]
		public virtual ICollection<Comment> Comments { get; set; }
    }
}