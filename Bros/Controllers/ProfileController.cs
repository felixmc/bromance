using Bros.DataModel;
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

        public ActionResult SendBroRequest(User receiver)
        {
            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                User user = (User)Session["User"];
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
                User user = (User)Session["User"];
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


                User user = (User)Session["User"];
                ViewBag.Bros = GetCircleByName(user, "Bros").Members;
                ViewBag.Request = request;
            }

            return View();
        }

        public void AddBroToCircle(string CircleName, User bro)
        {
            User user = (User)Session["User"];
            Circle targetCircle = GetCircleByName(user, CircleName);
            if (!targetCircle.Members.Contains(bro))
            {
                targetCircle.Members.Add(bro);
            }
        }

        public Circle GetCircleByName(User user, string CircleName)
        {
            return user.Circles.FirstOrDefault(m => m.Name == CircleName);
        }

        public void AcceptRequest(BroRequest request)
        {
            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                GetCircleByName(request.Sender, "Bros").Members.Add(request.Receiver); 
                GetCircleByName(request.Receiver, "Bros").Members.Add(request.Sender);
                request.RequestNotification.IsRead = true;
            }
        }

        public void DismissRequest(BroRequest request)
        {
            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                request.RequestNotification.IsRead = true;
            }
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