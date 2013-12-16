using Bros.DataModel;
using Bros.Enums;
using Bros.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WebMatrix.WebData;
namespace Bros.Controllers
{
	[AllowAnonymous]
	public class AuthenticationController : BroController
	{
		protected override void Initialize(RequestContext requestContext)
		{
			ViewBag.ContentClass = "widget wrapper condensed";
			base.Initialize(requestContext);
		}

		[HttpGet]
		public ActionResult Login()
		{
			ViewBag.Title = "Login";
			ViewBag.HideLogin = true;
			return View();
		}

		[HttpPost]
		public ActionResult Login(RegisterModel model)
		{
			bool isLoggedIn = model.Email != null && model.Password != null && WebSecurity.Login(model.Email, model.Password);
			if (isLoggedIn)
			{
				String redirectUrl = Request["ReturnUrl"] ?? "/Feed";
				return new RedirectResult(redirectUrl);
			}
			else
			{
				ViewBag.Error = "Invalid email or password.";
				ViewBag.Title = "Login";
				ViewBag.HideLogin = true;
				return View();
			}
		}

		public ActionResult Logout()
		{
			WebSecurity.Logout();
			Session.Clear();
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public bool IsEmailValid(string email = null)
		{
			email = email ?? Request["Email"];
			bool isValid = false;

			using (var context = new ModelFirstContainer())
			{
				isValid = context.Users.FirstOrDefault(u => u.Email.Equals(email)) == null;
			}

			return isValid;
		}

		[HttpGet]
		public ActionResult Register()
		{
			ViewBag.Title = "Register";
			return View();
		}

		[HttpPost]
		public ActionResult Register(RegisterModel model)
		{
			if (model.Gender == Gender.Female)
				return new RedirectResult(@"http://www.eharmony.com");

			if (ModelState.IsValid && IsEmailValid(model.Email))
			{
				try
				{
					string data = WebSecurity.CreateUserAndAccount(model.Email, model.Password, new { DateCreated = DateTime.Now, IsDeleted = false, IsBanned = false, IsMuted = false });
					Roles.AddUserToRole(model.Email, "User");

					using (var context = new ModelFirstContainer())
					{
						User newUser = context.Users.FirstOrDefault(u => u.Email.Equals(model.Email));

						Profile prof = new Profile()
						{
							FirstName = model.FirstName,
							LastName = model.LastName,
							BirthDate = model.BirthDate,
							ZipCode = model.Zipcode + "",
							Gender = Request["Gender"],
							User = newUser
						};
						prof.User = newUser;
						context.Profiles.Add(prof);

						ShoppingCart cart = new ShoppingCart() { User = newUser };
						newUser.ShoppingCart = cart;
						context.ShoppingCarts.Add(cart);

						Circle defaultFriendCircle = new Circle()
						{
							Name = "MyBros",
							Owner = newUser,
						};
						context.Circles.Add(defaultFriendCircle);

						Album album = new Album()
						{
							Owner = newUser,
							Title = "Profile Pictures",
							DateCreated = DateTime.Now
						};
						context.Albums.Add(album);

						context.SaveChanges();
					}

					WebSecurity.Login(model.Email, model.Password);
					return RedirectToAction("Edit", "Profile", new { firstTime = true });
				}
				catch (MembershipCreateUserException e)
				{
					ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
				}
			}

			ViewBag.Title = "Register";
			return View();
		}

		private static string ErrorCodeToString(MembershipCreateStatus createStatus)
		{
			switch (createStatus)
			{
				case MembershipCreateStatus.DuplicateUserName:
					return "User name already exists. Please enter a different user name.";

				case MembershipCreateStatus.DuplicateEmail:
					return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

				case MembershipCreateStatus.InvalidPassword:
					return "The password provided is invalid. Please enter a valid password value.";

				case MembershipCreateStatus.InvalidEmail:
					return "The e-mail address provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.InvalidAnswer:
					return "The password retrieval answer provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.InvalidQuestion:
					return "The password retrieval question provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.InvalidUserName:
					return "The user name provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.ProviderError:
					return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

				case MembershipCreateStatus.UserRejected:
					return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

				default:
					return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
			}
		}

	}
}