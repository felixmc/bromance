//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bros.DataSchema
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comment
    {
        public int ID { get; set; }
        public Nullable<int> Owner_ID { get; set; }
        public Nullable<int> ParentPost_ID { get; set; }
        public string Content { get; set; }
        public bool IsFlagged { get; set; }
    }
}
