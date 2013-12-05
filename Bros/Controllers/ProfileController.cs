﻿using Bros.DataModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Bros.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        //
        // GET: /Profile/

        [Authorize]
        public ActionResult ProfileIndex()
        {
            if (Session["UserId"] != null && ((int)Session["UserId"]) != 0)
            {
                int id = (int)Session["UserId"];
                User user = new User();
                using (var context = new ModelFirstContainer())
                {
                    user = context.Users.FirstOrDefault(x => x.Id == id);
                    ViewBag.LoginMessage = "Hello, " + user.Profile.FirstName;
                }
            }
            return View();
        }

        [Authorize]
        public ActionResult Feed()
        {
            List<Post> feedPosts = new List<Post>();

            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                int userId = (int)Session["UserId"];

                User user = context.Users.Where(u => u.Id == userId).FirstOrDefault();

                IEnumerable<int> feedMembers = user.Circles.Select(c => c.Members).SelectMany(u => u).Union(context.Users.Where(u => u.Id == userId)).Select(u => u.Id);
                List<Post> broPosts = context.Posts.Include("Comments.Owner.Profile").Include("Author.Profile")
                                            .Where(p => feedMembers.Contains(p.Author.Id))
                                                    .OrderByDescending(p => p.DateCreated)
                                                            .Take(30).ToList();

                feedPosts = broPosts.ToList();
            }

            return View(feedPosts);
        }

        #region BroRequest

        public ActionResult ViewBros()
        {
            IEnumerable<User> bros;

            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                int id = (int)Session["UserID"];
                //bros = context.Users.Where(u => u.Id != id).ToList();
                bros = context.Users.Include("Profile").Where(u => u.Id != id).ToList();


                List<Profile> profiles = new List<Profile>();

                foreach (User bro in bros)
                {
                    profiles.Add(bro.Profile);
                }

                ViewBag.Profiles = profiles;

            }

            return View(bros);
        }

        public ActionResult SendBroRequest(int recieverID)
        {
            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                int id = (int)Session["UserID"];
                User user = context.Users.FirstOrDefault(u => u.Id == id);

                User reciever = context.Users.FirstOrDefault(u => u.Id == recieverID);

                BroRequest request = CreateRequest(user, reciever);

                user.SentBroRequests.Add(request);
                //user.Notifications.Add(request.RequestNotification);

                reciever.ReceivedBroRequests.Add(request);
                //reciever.Notifications.Add(request.RequestNotification);

                ViewBag.Request = request;

                context.SaveChanges();
            }

            return View();
        }

        public void CreateCircle(string CircleName)
        {
            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                int id = (int)Session["UserID"];
                User user = context.Users.FirstOrDefault(u => u.Id == id);
                Circle targetCircle = new Circle();
                targetCircle.Name = CircleName;
                targetCircle.Owner = user;
                user.Circles.Add(targetCircle);

                context.SaveChanges();
            }
        }

        public BroRequest CreateRequest(User Sender, User Receiver)
        {
            BroRequest request = new BroRequest();

            request.DateCreated = DateTime.Now;
            request.Sender = Sender;
            request.Receiver = Receiver;

            request.RequestNotification = new Bros.DataModel.RequestNotification();
            request.RequestNotification.BroRequest = request;
            request.RequestNotification.DateCreated = DateTime.Now;
            request.RequestNotification.Receiver = Receiver;
            //request.RequestNotification.IsRead = false;

            request.Message = "Bro request from " + Sender.Profile.FirstName + " " + Sender.Profile.LastName + " to " + Receiver.Profile.FirstName + " " + Receiver.Profile.LastName + ".";

            return request;
        }

        public ActionResult BroAccept(int requestID)
        {

            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                BroRequest request = context.BroRequests.Include("Sender.Profile").Include("Receiver.Profile").FirstOrDefault(r => r.Id == requestID);
                AcceptRequest(request);

                int id = (int)Session["UserID"];
                User user = context.Users.Include("Circles").Include("Circles.Members.Profile").FirstOrDefault(u => u.Id == id);
                ViewBag.Bros = GetCircleByName(user, "MyBros").Members.Where(u => u.Id != user.Id).ToList();
                ViewBag.Request = request;

                context.SaveChanges();
            }

            return View();
        }

        public void AcceptRequest(BroRequest request)
        {
            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                AddBroToCircle("MyBros", request.Sender, request.Receiver);
                AddBroToCircle("MyBros", request.Receiver, request.Sender);

                //request.RequestNotification.IsRead = true;
            }
        }

        public void AddBroToCircle(string CircleName, User circleOwner, User broAdded)
        {
            Circle targetCircle = GetCircleByName(circleOwner, CircleName);
            if (!targetCircle.Members.Contains(broAdded))
            {
                targetCircle.Members.Add(broAdded);
            }
        }

        public Circle GetCircleByName(User user, string CircleName)
        {
            return user.Circles.FirstOrDefault(m => m.Name == CircleName);
        }

        public ActionResult DismissRequest(int requestID)
        {
            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                //request.RequestNotification.IsRead = true;
            }

            return RedirectToAction("ProfileIndex");
        }

        public ActionResult ViewBroRequests()
        {
            IEnumerable<BroRequest> unreadBroRequests;
            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                int id = (int)Session["UserID"];
                User user = context.Users.Include("ReceivedBroRequests.Sender.Profile").Include("ReceivedBroRequests.Receiver.Profile").FirstOrDefault(u => u.Id == id);

                unreadBroRequests = user.ReceivedBroRequests.ToList();
            }

            return View(unreadBroRequests);
        }

        #endregion

        [Authorize]
        public new ActionResult Profile()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult UpdateProfile(Profile profile)
        {
            return null;
        }

        [Authorize]
        [HttpPost]
        public ActionResult PostStatus()
        {
            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                int userId = (int)Session["UserId"];
                User user = context.Users.Where(u => u.Id == userId).FirstOrDefault();

                TextPost update = new TextPost() { Author = user, Content = Request["status"], DateCreated = DateTime.Now, DateUpdated = DateTime.Now };
                user.Posts.Add(update);

                context.SaveChanges();
            }

            if (Request.IsAjaxRequest())
                return null;
            else
                return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        [Authorize]
        [HttpPost]
        public ActionResult PostComment()
        {
            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                int postId = Int32.Parse(Request["post"]);

                Post post = context.Posts.Where(p => p.Id == postId).FirstOrDefault();

                if (post != null)
                {
                    int userId = (int)Session["UserId"];
                    User user = context.Users.Where(u => u.Id == userId).FirstOrDefault();
                    Comment comment = new Comment() { Content = Request["comment"], Owner = user, ParentPost = post, DateCreated = DateTime.Now };

                    user.Comments.Add(comment);
                    post.Comments.Add(comment);

                    context.SaveChanges();
                }

            }

            if (Request.IsAjaxRequest())
                return null;
            else
                return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditProfile()
        {

            User user = null;
            int id = 0;
            using (var context = new ModelFirstContainer())
            {
                id = (int)Session["UserId"];

                user = context.Users.FirstOrDefault(x => x.Id == id);
                if (user == null) throw new Exception("Session not set exception");
                return View("ProfileAttribute", user.Profile);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditProfileAttributes(Profile prof)
        {

            if (Session["UserId"] != null)
            {
                int id = (int)Session["UserId"];

                if (ModelState.IsValid)
                    using (ModelFirstContainer context = new ModelFirstContainer())
                    {
                        User user = context.Users.FirstOrDefault(x => x.Id == id);
                        user.Profile.Athleticism = prof.Athleticism;
                        user.Profile.Children = prof.Children;
                        user.Profile.Drinks = prof.Drinks;
                        user.Profile.Drugs = prof.Drugs;
                        user.Profile.Education = prof.Education;
                        user.Profile.Ethnicity = prof.Ethnicity;
                        user.Profile.Job = prof.Job;
                        user.Profile.MarriageStatus = prof.MarriageStatus;
                        user.Profile.Pets = prof.Pets;
                        user.Profile.Religion = prof.Religion;
                        user.Profile.SexualOrientation = prof.SexualOrientation;
                        user.Profile.Smokes = prof.Smokes;


                        context.SaveChanges();
                    }

                return View("ProfileIndex");
            }
            else
                throw new Exception("Session is null, or user not logged in.");

        }

        [AllowAnonymous]
        public ActionResult MyProfile()
        {
            User user = new User();
            int profileId = 0;
            if (Session["UserId"] != null)
            {
                profileId = ((int)Session["UserId"]);
                using (var context = new ModelFirstContainer())
                {
                    user = context.Users.Include("Albums").FirstOrDefault(u => u.Id == profileId);

                    Bros.DataModel.Photo profilePic = (Photo)user.Posts.FirstOrDefault(p => p.IsDeleted == true);
                    Bros.DataModel.Photo post = profilePic;
                    if (post == null)
                    {
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
                            Owner = user,
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
                            Author = user,
                            ProfilePhotoOf = user.Profile,
                            Album = album

                        };
                        context.Posts.Add(defaultPhoto);
                        context.SaveChanges();
                    }
                }
            }
            return View(user);
        }

        [Authorize]
        [HttpGet]
        public ActionResult ChangeProfilePhoto()
        {

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangeProfilePhoto(User user)
        {

            return View();
        }

    }
}