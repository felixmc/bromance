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

        private static byte[] salt = AuthenticationController.CreateSalt(256);
        static User _User = new User()
        {
            Salt = salt,
            Password = AuthenticationController.GeneratedSaltedHash("Donkey", salt),
            Email = "BroBWishing@gmail.com",
            DateCreated = DateTime.Today

        };
        public User User
        {
            get
            {
                return _User;
            }
            set
            {
                if (User == null)
                {
                    User = _User;
                }
            }
        }

        static DateTime birth = new DateTime(1994, 4, 12);
        static Profile _Profile = new Profile()
        {
            FirstName = "BroDude",
            LastName = "Browser",
            ZipCode = "12345",
            BirthDate = birth,
            Gender = "Male",
            Pets = "None",
            Religion = "Agnostic",
            Job = "Brobo",
            Education = "NU",
            Ethnicity = "AZN",
            Athleticism = "Built",
            SexualOrientation = "Straight",
            MarriageStatus = "Open",
            Children = "No way",
            Smokes = "Meh",
            Drinks = "Definitely",
            Drugs = "Nahh dude.",

            User = _User,


        };
        public Profile Profile
        {
            get
            {
                return _Profile;
            }
            set
            {
                if (Profile == null)
                {
                    Profile = _Profile;
                }
            }
        }

        static List<Profile> InterestedList = new List<Profile>()
        {
            _Profile
        };
        static Interest _Interest = new Interest()
        {
            Name = "Being a Bro",
            InterestedProfiles = InterestedList

        };
        public Interest Interest
        {
            get
            {
                return _Interest;
            }
            set
            {
                if (Interest == null)
                {
                    Interest = _Interest;
                }
            }
        }


    }
}
