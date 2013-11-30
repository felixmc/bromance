﻿using Bros.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bros.Controllers
{
	public class ProfileController : Controller
	{
		//
		// GET: /Profile/

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

		public ActionResult Feed()
		{
			List<Post> feedPosts = new List<Post>();

			using (ModelFirstContainer context = new ModelFirstContainer())
			{
				User user = (User)Session["User"];
				ICollection<Post> broPosts = user.Circles.Select(c => c.Members).SelectMany(u => u)
												.Select(u => u.Posts).SelectMany(p => p)
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
                bros = context.Users.Where(u => u.Id != id).ToList();
            }

            return View(bros);
        }

        public ActionResult SendBroRequest(User receiver)
        {
            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                int id = (int)Session["UserID"];
                User user = context.Users.FirstOrDefault(u => u.Id == id);
                BroRequest request = CreateRequest(user, receiver);

                user.SentBroRequests.Add(request);
                user.Notifications.Add(request.RequestNotification);

                receiver.ReceivedBroRequests.Add(request);
                receiver.Notifications.Add(request.RequestNotification);

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

        public BroRequest CreateRequest(User Sender, User Reciever)
        {
            BroRequest request = new BroRequest();

            request.Sender = Sender;
            request.Receiver = Reciever;

            request.RequestNotification = new Bros.DataModel.RequestNotification();
            request.RequestNotification.BroRequest = request;
            request.RequestNotification.IsRead = false;

            request.Message = "Bro request from " + Sender.Profile.FirstName + " " + Sender.Profile.LastName + " to " + Reciever.Profile.FirstName + " " + Reciever.Profile.LastName + ".";

            return request;
        }

        public ActionResult BroAccept(BroRequest request)
        {

            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                AcceptRequest(request);

                int id = (int)Session["UserID"];
                User user = context.Users.FirstOrDefault(u => u.Id == id);
                ViewBag.Bros = GetCircleByName(user, "MyBros").Members;
                ViewBag.Request = request;
            }

            return View();
        }

        public void AcceptRequest(BroRequest request)
        {
            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                AddBroToCircle("MyBros", request.Sender, request.Receiver);
                AddBroToCircle("MyBros", request.Receiver, request.Sender);

                request.RequestNotification.IsRead = true;
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

        public ActionResult DismissRequest(BroRequest request)
        {
            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                request.RequestNotification.IsRead = true;
            }

            return RedirectToAction("ProfileIndex");
        }

        public ActionResult ViewBroRequests()
        {
            IEnumerable<BroRequest> unreadBroRequests;
            using(ModelFirstContainer context = new ModelFirstContainer()){
                int id = (int)Session["UserID"];
                User user = context.Users.FirstOrDefault(u => u.Id == id);

                unreadBroRequests = user.ReceivedBroRequests.Where(m => m.RequestNotification.IsRead == false).ToList();
            }

            return View(unreadBroRequests);
        }

        #endregion

        public new ActionResult Profile()
		{
			return View();
		}

		[HttpPost]
		public ActionResult UpdateProfile(Profile profile)
		{
			return null;
		}

		[HttpPost]
		public ActionResult PostStatus()
		{
			using (ModelFirstContainer context = new ModelFirstContainer())
			{
				User user = (User)Session["User"];

				TextPost update = new TextPost() { Author = user, Content = Request["status"], DateCreated = new DateTime() };
				user.Posts.Add(update);

				context.SaveChanges();
			}

			if (Request.IsAjaxRequest())
				return null;
			else
				return Redirect(Request.UrlReferrer.AbsolutePath);
		}

		[HttpPost]
		public ActionResult PostComment()
		{
			using (ModelFirstContainer context = new ModelFirstContainer())
			{
				int postId = Int32.Parse(Request["post"]);

				Post post = context.Posts.Where(p => p.Id == postId).FirstOrDefault(null);

				if (post != null)
				{
					User user = (User)Session["User"];
					Comment comment = new Comment() { Content = Request["comment"], Owner = user, ParentPost = post, DateCreated = new DateTime() };

					user.Comments.Add(comment);

					context.SaveChanges();
				}

			}

			if (Request.IsAjaxRequest())
				return null;
			else
				return Redirect(Request.UrlReferrer.AbsolutePath);
		}

        [HttpGet]
        public ActionResult EditProfile()
        {

            User user = null;
            int id = 0;
            using(var context = new ModelFirstContainer()){
            id = (int)Session["UserId"];
          
                user = context.Users.FirstOrDefault(x => x.Id == id); 
                if (user == null) throw new Exception("Session not set exception");
            return View("ProfileAttribute", user.Profile);
            }

            
            
        }

        [HttpPost]
        public ActionResult EditProfileAttributes(Profile prof)
        {

            if(ModelState.IsValid)
            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                

            }

            return View("ProfileIndex");
        }

	}
}