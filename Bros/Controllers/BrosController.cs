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
	[Authorize(Roles="User")]
	public class BrosController : BroController
	{
		
		public ActionResult Search()
		{
			String param = (Request["query"] ?? "").ToLower();
			List<User> users = new List<User>();
			using (var context = new ModelFirstContainer())
			{
				users = context.Users.Include("Profile.ProfilePhoto").Where(x => x.Email.ToLower().Contains(param) ||
														x.Profile.FirstName.ToLower().Contains(param) ||
														x.Profile.LastName.ToLower().Contains(param)).ToList<User>();
			}

			ViewBag.Query = param;
			ViewBag.Title = "Search Bros";
			ViewBag.ContentClass = "semi-condensed";

			return View(users);
		}

        [HttpGet]
		public ActionResult Match()
		{
			Profile prof = new Profile();
			using (var context = new ModelFirstContainer())
			{
				prof = context.Profiles.FirstOrDefault(u => u.User.Id == WebSecurity.CurrentUserId);
			}

            ViewBag.SubTitle = "What do you prefer in others?";
			ViewBag.Title = "Match Bros";

			return View(prof);
		}

        [HttpPost]
        public ActionResult Match(Profile profile, bool searchInArea)
        {

            bool searchArea = searchInArea;
            Dictionary<User, double> scoreList = new Dictionary<User, double>();
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
            if (searchInArea)
            {
                scoreList = match.BaseScoreCompatiblity(preferences, true);

                if (scoreList.Count == 0) ViewBag.Message = "0 No one lives near you";
            }
            else
            {
                scoreList = match.BaseScoreCompatiblity(preferences, false);
                if (scoreList.Count == 0) ViewBag.Message = "No one would like you";
            }

            ViewBag.Title = "Search Results";
            IOrderedEnumerable<KeyValuePair<User, double>> sortedScoreList = from entry in scoreList where entry.Value > 10 orderby entry.Value descending select entry;
            
            return View("_SearchResults", sortedScoreList);
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

			ViewBag.Title = "Browse Bros";
			ViewBag.ContentClass = "semi-condensed";

			return View(bros);
		}

	}
}