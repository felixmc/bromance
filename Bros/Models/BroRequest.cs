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
        [InverseProperty("Id")]
        [ForeignKey("Sender")]
		public virtual User Sender { get; set; }
        [InverseProperty("Id")]
        [ForeignKey("Receiver")]
		public virtual User Receiver { get; set; }
        public string Message { get; set; }
    }
}