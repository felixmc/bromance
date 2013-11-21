using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    [Table("BroRequest")]
    public class BroRequest : Entity
    {
        [InverseProperty("ID")]
        [ForeignKey("Sender")]
		public virtual User Sender { get; set; }
        [InverseProperty("ID")]
        [ForeignKey("Receiver")]
		public virtual User Receiver { get; set; }
        public string Message { get; set; }
    }
}