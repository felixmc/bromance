using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class Profile
    {
        public User Owner { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] Hobbies { get; set; } //made this a string, may later change to enum
        public string ZipCode { get; set; } //need to force this to only be 5 long and numbers
        public DateTime Birthdate { get; set; }
        // TODO add gender enums
        public Liking[] Likings { get; set; }
        // TODO add Job /Career Maybe Enums or string arrays?
        // TODO add pet Enums
        // TODO add Religion Enums
        // TODO add Education Enums
        // TODO add Sexual Orentation Enums
        // TODO add Ethnicity Enums
        // TODO add Athleticism Enums
        // TODO add Marriage Status Enums or do we want to do a true false?
        // TODO add Chilrdren Enums or do we want to do a true false?
        









    }
}