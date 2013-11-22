using Bros.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bros.Controllers
{
    public class AuthenticationController : Controller
    {
        //
        // GET: /Authentication/

        public ActionResult Index()
        {
            string password = this.Request["password"];
            string username = this.Request["username"];

            if (Login(password, username))
            {

            }
            HomeController controller = new HomeController();
            return controller.Index();
        }

        private bool Login(string password, string username)
        {
            bool isLoggedIn = false;
			//using (BroContext context = new BroContext())
			//{
			//	Models.User user = ((IEnumerable<Models.User>)context.Entities.Where(n => n is Models.User && ((Models.User)n).Email == username)).First();
			//	byte[] enteredPassword = Models.User.GeneratedSaltedHash(password, user.salt);
			//	if (Models.User.CompareByteArrays(enteredPassword, user.password))
			//	{
			//		//LoginUser();
			//		isLoggedIn = true;
			//	}

			//}
            return isLoggedIn;
        }

    }
}
