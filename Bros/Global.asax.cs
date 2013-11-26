using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using WebMatrix.WebData;

namespace Bros
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
<<<<<<< HEAD
			WebSecurity.InitializeDatabaseConnection("ModelFirstContainer", "Users", "Id", "Email", false);
=======
            WebSecurity.InitializeDatabaseConnection("ModelFirstContainer", "UserAuthentication", "Id", "Email", true);
>>>>>>> d6a94785beeaf4d6778b4caddc52075011df38b1
			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
		}
	}
}