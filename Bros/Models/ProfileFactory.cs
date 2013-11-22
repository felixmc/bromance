using Bros.Controllers;
using Bros.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class ProfileFactory
    {

        Profile _Profile = new Profile()
        {
            FirstName = "BroDude",
            LastName = "Browser"
        };
        public Profile Profile
        {
              get
            {
                return Profile;
            }
            set
            {
                if (Profile == null){
                    Profile = _Profile;
                }
            }
        }


        User _User = new User()
        {
            //Salt = AuthenticationController
            //Password = 

        };
        public User User
        {
            get
            {
                return User;
            }
            set
            {
                if (User == null){
                    User = _User;
                }
            }
        }
    }
}