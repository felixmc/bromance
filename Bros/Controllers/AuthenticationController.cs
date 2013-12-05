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
            string loginMessage = "";
            bool isLoggedIn = WebSecurity.Login(model.Email, model.Password);
            if (isLoggedIn)
            {
                using (ModelFirstContainer context = new ModelFirstContainer())
                {
                    Bros.DataModel.User user =
                        (from u in context.Users
                         where u.Email == model.Email
                         select u).FirstOrDefault<User>();
                    Session.Add("UserId", user.Id);
                    
                    loginMessage = "Welcome, " + user.Profile.FirstName + "! You are logged in!";
                }
                ViewBag.LoginMessage = loginMessage;

                return RedirectToAction("Feed", "Profile");
            }
            else
            {

                ViewBag.Error = "Invalid username or password.";

                return View();
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.LoginMessage = "Enter Name";
            return View();
        }

		public ActionResult Logout()
		{
			WebSecurity.Logout();
			Session.Clear();
			return RedirectToAction("Index", "Home");
		}

        //[HttpPost]
        //public ActionResult Register(RegisterModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Attempt to register the user
        //        try
        //        {
        //            WebSecurity.CreateUserAndAccount(model.Email, model.Password);
        //            WebSecurity.Login(model.Email, model.Password);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        catch (MembershipCreateUserException e)
        //        {
        //            ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

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

		[HttpPost]
		public ActionResult RegisterUser(RegisterModel model)
		{
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    string data = WebSecurity.CreateUserAndAccount(model.Email, model.Password, new { dateCreated = DateTime.Now, isbanned = false, isdeleted = false });
                    //WebSecurity.Login(model.Email, model.Password);
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }
            else
                return View("Register");

			using (var context = new ModelFirstContainer())
			{
                
                Profile prof = new Profile()
                {   
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    BirthDate = model.BirthDate,
                    ZipCode = model.Zipcode + "",
                    Gender = Request["Gender"],
                    
                };

				User newUser = context.Users.FirstOrDefault(u=>u.Email.Equals(model.Email));

                if (newUser != null)
                {

                    prof.User = newUser;

                    newUser.Profile = prof;

                    context.Profiles.Add(prof);

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
                }
                else
                {
                    throw new Exception("User email was not added to db");
                }

                Circle defaultFriendCircle = new Circle()
                {
                    Name ="MyBros",
                    Owner = newUser,
                };
                context.Circles.Add(defaultFriendCircle);


                var imagePath = Server.MapPath("~/Content/Images/defaultPic.jpg");
                Image img = Image.FromFile(imagePath);
                byte[] arr;
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    arr = ms.ToArray();
                } 
                 ICollection<Photo> photoAlbum = new List<Photo>();
                Album album = new Album()
                {
                    Owner = newUser,
                    Title = "Default Picture",
                    DateCreated = DateTime.Today,
                    Photos = photoAlbum
                };
                context.Albums.Add(album);
                context.SaveChanges();

                Photo defaultPhoto = new Photo()
                {
                    ImageData = arr,
                    DateCreated = DateTime.Today,
                    DateUpdated = DateTime.Today,
                    Caption = "Default",
                    IsDeleted = true,
                    Author = newUser,
                    ProfilePhotoOf = prof,
                    Album = album
                    
                };
                context.Posts.Add(defaultPhoto);
                context.SaveChanges();


			}

            return RedirectToAction("Index", "Home");
		}

    }
}
