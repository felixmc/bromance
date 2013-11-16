using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public enum Gender
    {
        Female, Male
    }

    public class Profile : Entity
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Hobby> HobbyList { get; set; }
        public int Zipcode { get; set; }
        public DateTime MyProperty { get; set; }
        public Gender Gender { get; set; }
        
    }
}