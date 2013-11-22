using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bros.Models
{
    public class ProfileFactory
    {

        public Profile GetProfile()
        {
            Profile prof = new Profile()
            {

            };

            return prof;
        }

        public User GetUser()
        {
            User use = new User()
            {

            };

            return use;
        }
    }
}