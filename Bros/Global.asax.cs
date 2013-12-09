using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WebMatrix.WebData;

namespace Bros
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801
	public class MvcApplication : System.Web.HttpApplication
	{
        protected void Application_Start()
        {
            WebSecurity.InitializeDatabaseConnection("WebSecurityConectionString", "Users", "Id", "Email", true);
            AreaRegistration.RegisterAllAreas();

            string[] roles = {
				"User",
                "Admin",
                "StoreAdmin"
			};

            var roleProvider = (SimpleRoleProvider)Roles.Provider;
            foreach (string role in roles)
            {
                if (!roleProvider.RoleExists(role))
                {
                    roleProvider.CreateRole(role);
                }
            }

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
	}
}