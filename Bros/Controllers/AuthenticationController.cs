using Bros.DataModel;
using Bros.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
namespace Bros.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        //
        // GET: /Authentication/
        [HttpPost]
        public ActionResult Login(RegisterModel model)
        {
            bool isLoggedIn = WebSecurity.Login(model.Email, model.Password);
            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                Bros.DataModel.User user = 
                    (from u in context.Users
                    where u.Email == model.Email
                    select u).FirstOrDefault<User>();
                Session.Add("User", user);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    WebSecurity.CreateUserAndAccount(model.Email, model.Password);
                    WebSecurity.Login(model.Email, model.Password);
                    return RedirectToAction("Index", "Home");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
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


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        public static byte[] GeneratedSaltedHash(string plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] plainTextWithSaltedBytes = new byte[plainTextBytes.Length + salt.Length];

            for (int i = 0; i < plainTextBytes.Length; i++)
            {
                plainTextWithSaltedBytes[i] = plainTextBytes[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltedBytes[i + plainTextBytes.Length] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltedBytes);
        }

        public static bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
                return false;

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                    return false;
            }

            return true;
        }

        public static byte[] CreateSalt(int size)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[size];
            rng.GetBytes(salt);

            return salt;
        }

		[HttpPost]
		public ActionResult UserForm(RegisterModel model)
		{
			using (var context = new ModelFirstContainer())
			{
				User user = new User();

				context.Users.Add(user);
				try
				{
					context.SaveChanges();
				}
				catch (DbEntityValidationException e)
				{
					foreach (var validationErrors in e.EntityValidationErrors)
					{
						foreach (var validationError in validationErrors.ValidationErrors)
						{
							Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
						}
					}
				}

				//context.SaveChanges();

			}

			return View();
		}

    }
}
