using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bros.DataModel;
using Bros.Models;
using WebMatrix.WebData;

namespace Bros.Controllers
{
    public class BrosController : BroController
    {
        [Authorize]
        public ActionResult ByUserName()
        {
            String param = Request["searchParam"];
            var context = new ModelFirstContainer();
            var users = context.Users.Where(x => x.Email.Contains(param)).ToList<User>();
            return View("SearchResults",users);
        }

        public ActionResult DesireSearch()
        {

            Profile prof = new Profile();
            using (var context = new ModelFirstContainer())
            {
                prof = context.Profiles.FirstOrDefault(u => u.User.Id == WebSecurity.CurrentUserId);
            }
            
            return View(prof);
        }


        [HttpPost]
        public ActionResult MatchBros(Profile profile)
        {
            int score = 0;
            User thisUser = new User();
            List<User> compatibleUsers = new List<User>();
            Dictionary<User, double> scoreList = new Dictionary<User, double>();
            using(var context = new ModelFirstContainer()){
                thisUser = context.Users.FirstOrDefault(u => u.Id == WebSecurity.CurrentUserId);
                compatibleUsers = context.Users.Include("Profile").ToList();
            }

            Dictionary<string, string> preferences = new Dictionary<string, string>()
            {
                    {"Athleticism", profile.Athleticism},
                     {"Pets", profile.Pets},
                      {"Religion", profile.Religion},
                       {"Education", profile.Education},
                        {"Ethnicity", profile.Ethnicity},
                        {"SexualOrientation", profile.SexualOrientation},
                         {"MarriageStatus", profile.MarriageStatus},
                          {"Children", profile.Children},
                           {"Smokes", profile.Smokes},
                            {"Drinks", profile.Drinks},
                             {"Drugs", profile.Drugs},

            };

            Matcher match = new Matcher();
            scoreList = match.BaseScoreCompatiblity(preferences);

            IOrderedEnumerable<KeyValuePair<User, double>> sortedScoreList = from entry in scoreList orderby entry.Value descending select entry;
            
            return View("SearchResults", sortedScoreList);
        }


        public List<User> ByPreferences()
        {
            List<User> compatibleUsers = new List<User>();



            return compatibleUsers;
        }

		public ActionResult Browse()
		{
			List<User> bros = new List<User>();

			using (ModelFirstContainer context = new ModelFirstContainer())
			{
				int id = WebSecurity.CurrentUserId;
				User thisUser = context.Users.FirstOrDefault(u => u.Id == id);
				bros = context.Users.Include("Profile.ProfilePhoto").Where(u => u.Id != id).ToList();
			}

			return View(bros);
		}

    }
}