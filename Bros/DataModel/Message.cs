//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bros.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Message
    {
       
        
        public Message()
        {
            DateSent = new DateTime();

        }
        public int Id { get; set; }
        public string Content { get; set; }
        public System.DateTime DateSent { get; set; }
        public System.DateTime DateRead { get; set; }
    
        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }
    }
}
