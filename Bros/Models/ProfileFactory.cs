using Bros.Controllers;
using Bros.DataModel;
using Bros.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Bros.Models
{
    public static class ProfileFactory
    {
        static Random rand = new Random();
        public static User User
        {
            get
            {
                User _User = new User () {
					Email = RandomWord() + "@gmail.com",
					DateCreated = DateTime.Today,
				};

				Profile.User = _User;
				_User.Profile = Profile;

				return _User;
            }
        }

        static DateTime birth = new DateTime(1994, 4, 12);

        public static Profile Profile
        {
            get
            { 
            Profile _Profile = new Profile(){
            FirstName = RandomWord(),
            LastName = RandomWord(),
            ZipCode = RandomZip(),
            BirthDate = birth,
            Gender = RandomEnum<Gender>(),
            Pets = RandomEnum<Pets>(),
            Religion = RandomEnum<Religion>(),
            Job = RandomEnum<Job>(),
            Education = RandomEnum<Education>(),
            Ethnicity = RandomEnum<Ethnicity>(),
            Athleticism = RandomEnum<Athleticism>(),
            SexualOrientation = RandomEnum<SexualOrientation>(),
            MarriageStatus = RandomEnum<MariageStatus>(),
            Children = RandomEnum<Children>(),
            Smokes = RandomEnum<Smokes>(),
            Drinks = RandomEnum<Drinks>(),
            Drugs = RandomEnum<Drugs>(),
            };
                return _Profile;
            }
        }

        static List<Profile> InterestedList = new List<Profile>()
        {
           
        };
        static Interest _Interest = new Interest()
        {
            Name = "Being a Bro",
            InterestedProfiles = InterestedList

        };
        static public Interest Interest
        {
            get
            {
                return _Interest;
            }
        }

        static private string RandomEnum<T>()
        {

            T[] items = (T[])Enum.GetValues(typeof(T));

            return items[rand.Next(0, items.Length)].ToString();
            
        }

        static string RandomZip()
        {
            string zip = "";
            for (int i = 0; i < 5; i++)
            {
                int x = rand.Next(0, 10);
                zip += (x + "");
            }

                return zip;
        }

        static string RandomWord()
        {
            string word = "";
            int length = rand.Next(4, 15);

            for (int i = 0; i < length; i++)
            {
                word += RandomLetter(i);
            }


                return word;
        }

        static char RandomLetter(int index)
        {
            char letter = ' ';
            if (index == 0)
            {
                letter = (char)rand.Next(65, 91);
            }
            else
            {
                letter = (char)rand.Next(97,123);
            }

            return letter;
        }


    }
}
