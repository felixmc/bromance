using Bros.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace Bros.Models
{
    public class Matcher
    {

        public Matcher()
        {

        }

        public Dictionary<User,double> BaseScoreCompatiblity(Dictionary<string, string> lookFor, bool searchInArea)
        {
            double score = 0;
            User thisUser = new User();
            List<User> compatibleUsers = new List<User>();
            List<string> keys = new List<string>();
            using (var context = new ModelFirstContainer())
            {
                thisUser = context.Users.Include("Profile").FirstOrDefault(u => u.Id == WebSecurity.CurrentUserId);

                foreach (KeyValuePair<string, string> x in lookFor)
                {
                    keys.Add(x.Key);

                    Bros.DataModel.Preference pref = new Bros.DataModel.Preference()
                    {
                        Key = x.Key,
                        Value = x.Value,
                        UserId = thisUser.Id
                    };

                    context.Preferences.Add(pref);
                    context.SaveChanges();
                }

                if (!searchInArea)
                    compatibleUsers = context.Users.Include("Profile.ProfilePhoto").Where(u => u.Id != thisUser.Id).ToList();
                else
                    compatibleUsers = context.Users.Include("Profile.ProfilePhoto").Where(u => u.Id != thisUser.Id && u.Profile.ZipCode.Equals(thisUser.Profile.ZipCode)).ToList();
            }
            Dictionary<User, double> scoreList = new Dictionary<User, double>();

            foreach(User user in compatibleUsers){
                score = 0; Dictionary<string, string> profileList = new Dictionary<string, string>()
                {
                   {"Athleticism",user.Profile.Athleticism},
                     {"Pets", user.Profile.Pets},
                      {"Religion", user.Profile.Religion},
                       {"Education", user.Profile.Education},
                        {"Ethnicity", user.Profile.Ethnicity},
                        {"SexualOrientation", user.Profile.SexualOrientation},
                         {"MarriageStatus", user.Profile.MarriageStatus},
                          {"Children", user.Profile.Children},
                           {"Smokes", user.Profile.Smokes},
                            {"Drinks", user.Profile.Drinks},
                             {"Drugs", user.Profile.Drugs},
                };
                foreach(string key in keys){
                    if (lookFor[key].Equals(profileList[key])) score++;
                }
                scoreList.Add(user, ((double)score/11*100));
            }

            return scoreList;
        }

        public List<Compatibility> DetermineCompatability(User currentUser, List<User> browseList)
        {
            List<Compatibility> compatabilityList = new List<Compatibility>();

            foreach (var user in browseList)
            {
                int compatible = CompareProfiles(currentUser, user);
                compatabilityList.Add(new Compatibility() { CompatibilityOfUser = compatible, User = user });
            }
            return sortCompatabilityList(compatabilityList);
        }

        private List<Compatibility> sortCompatabilityList(List<Compatibility> compatabilityList)
        {
            return MergeSort(compatabilityList, 0, compatabilityList.Count - 1);
        }

        private List<Compatibility> MergeSort(List<Compatibility> compatibility, int left, int right)
        {
            int mid;
            if (right > left)
            {
                mid = (right + left) / 2;
                MergeSort(compatibility, left, mid);
                MergeSort(compatibility, mid + 1, right);

                Merge(compatibility, left, (mid + 1), right);
            }
            return compatibility;
        }

        private void Merge(List<Compatibility> compatibility, int left, int mid, int right)
        {
            //List<Compatibility> temp = new List<Compatibility>();
            //int left_end, num_elements, tmp_pos;
            //left_end = (mid - 1);
            //tmp_pos = left;
            //num_elements = (right - left + 1);

            //while ((left <= left_end) && (mid <= right))
            //{
            //	if (compatibility[left].CompatibilityOfUser <= compatibility[mid].CompatibilityOfUser)
            //		temp[tmp_pos++] = compatibility[left++];
            //	else
            //		temp[tmp_pos++] = compatibility[mid++];
            //}

            //while (left <= left_end)
            //	temp[tmp_pos++] = compatibility[left++];
            //while (mid <= right)
            //	temp[tmp_pos++] = compatibility[mid++];

            //for (int i = 0; i < num_elements; i++)
            //{
            //	compatibility[right] = temp[right];
            //	right--;
            //}
        }

        private int CompareProfiles(User currentUser, User user)
        {
            const int totalPreference = 12;
            int compatablePreference = 0;

            Profile userProfile = user.Profile;
            Profile currentUserProfile = currentUser.Profile;

            if (currentUserProfile.Athleticism.Equals(userProfile.Athleticism))
                compatablePreference++;
            if (currentUserProfile.Children.Equals(userProfile.Children))
                compatablePreference++;
            if (currentUserProfile.Drinks.Equals(userProfile.Drinks))
                compatablePreference++;
            if (currentUserProfile.Drugs.Equals(userProfile.Drugs))
                compatablePreference++;
            if (currentUserProfile.Education.Equals(userProfile.Education))
                compatablePreference++;
            if (currentUserProfile.Ethnicity.Equals(userProfile.Ethnicity))
                compatablePreference++;
            if (currentUserProfile.Job.Equals(userProfile.Job))
                compatablePreference++;
            if (currentUserProfile.MarriageStatus.Equals(userProfile.MarriageStatus))
                compatablePreference++;
            if (currentUserProfile.Pets.Equals(userProfile.Pets))
                compatablePreference++;
            if (currentUserProfile.Religion.Equals(userProfile.SexualOrientation))
                compatablePreference++;
            if (currentUserProfile.Religion.Equals(userProfile.Religion))
                compatablePreference++;
            if (currentUserProfile.Smokes.Equals(userProfile.Smokes))
                compatablePreference++;
            double compatability = compatablePreference / totalPreference;
            return (int)(compatability * 100);
        }
    }
}