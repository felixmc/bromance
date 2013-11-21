﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    [Table("Message")]
    public class Message : Entity
    {
        [InverseProperty("ID")]
        [ForeignKey("Sender")]
		public virtual User Sender { get; set; }
        [InverseProperty("ID")]
        [ForeignKey("Receiver")]
		public virtual User Receiver { get; set; }
        public string Content { get; set; }
        public DateTime DateSeen { get; set; }
    }
}