using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    [Table("Notification")]
    public class Notification : Entity
    {
        [InverseProperty("Id")]
        [ForeignKey("User")]
		public virtual User Receiver { get; set; }
        public bool IsRead { get; set; }
    }
}