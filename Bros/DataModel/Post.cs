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
    
    public partial class Post
    {
        public Post()
        {
            this.Comments = new HashSet<Comment>();
        }
    
        public int Id { get; set; }
        public bool IsFlagged { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateUpdated { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual User Author { get; set; }
    }
}
