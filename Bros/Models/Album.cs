using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    [Table("Album")]
    public class Album : Entity
    {
        [InverseProperty("Id")]
        [ForeignKey("User")]
		public virtual User Owner { get; set; }
        public string Title { get; set; }

        [InverseProperty("Id")]
        [ForeignKey("PhotoId")]
		public virtual ICollection<Photo> Photos { get; set; }
    }
}