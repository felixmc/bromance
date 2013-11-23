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
    
    public partial class Profile
    {
        public Profile()
        {
            this.Interests = new HashSet<Interest>();
        }
    
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ZipCode { get; set; }
        public System.DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Pets { get; set; }
        public string Religion { get; set; }
        public string Job { get; set; }
        public string Education { get; set; }
        public string Ethnicity { get; set; }
        public string Athleticism { get; set; }
        public string SexualOrientation { get; set; }
        public string MarriageStatus { get; set; }
        public string Children { get; set; }
        public string Smokes { get; set; }
        public string Drinks { get; set; }
        public string Drugs { get; set; }
    
        public virtual User Owner { get; set; }
        public virtual ICollection<Interest> Interests { get; set; }
        public virtual Photo ProfilePhoto { get; set; }
    }
}