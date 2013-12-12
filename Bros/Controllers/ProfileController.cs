using Bros.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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

        public ActionResult Settings(int id)
        {
            User user = new User();
            using(var context = new ModelFirstContainer()){
                user = context.Users.Include("Profile").Include("BlockedBros.Profile").FirstOrDefault(u => u.Id == id);
            }
            return View(user);
        }

        #region BroRequest

        public ActionResult BlockBro(int id)
        {
            
            using(var context = new ModelFirstContainer()){
                int userId = WebSecurity.CurrentUserId;
                User blockedUser = context.Users.FirstOrDefault(u => u.Id == id);
                User thisUser = context.Users.FirstOrDefault(u => u.Id == userId);
                thisUser.BlockedBros.Add(blockedUser);

                IEnumerable<Circle> circleList = context.Circles.Where(c => c.Owner.Id == thisUser.Id);
                foreach(Circle circle in circleList){
                    circle.Members.Remove(blockedUser);
                }
                context.SaveChanges();
               
            }
            return RedirectToAction("ViewBros", "Profile");
        }

        public ActionResult UnblockBro(int id)
        {
            User blockedUser = new User();
            User thisUser = new User();
            using(var context = new ModelFirstContainer()){
                blockedUser = context.Users.Include("BlockedBros.Profile").FirstOrDefault(u => u.Id == id);
                thisUser = context.Users.Include("BlockedBros.Profile").FirstOrDefault(u => u.Id == WebSecurity.CurrentUserId);
                thisUser.BlockedBros.Remove(blockedUser);
                context.SaveChanges();
                ViewBag.Success = "You have unblocked " + blockedUser.Profile.FirstName + " " + blockedUser.Profile.LastName;
            }
            
            return View("Settings", thisUser);
        }

        public ActionResult ViewBros()
        {
            List<User> bros;
            List<User> browseList = new List<User>();
            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                int id = (int)Session["UserID"];
                User thisUser = context.Users.FirstOrDefault(u => u.Id == id);
                //bros = context.Users.Where(u => u.Id != id).ToList();
                bros = context.Users.Include("Profile").Where(u => u.Id != id).ToList();
                browseList = bros.ToList();
                foreach (User bro in bros)
                {
                    foreach(User blockedBro in thisUser.BlockedBros){
                        if (bro.Id == blockedBro.Id)
                        {
                            browseList.Remove(bro);
                            break;
                        }
                    }
                }


            }

            return View(browseList);
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

        public void RemoveBro(int targetBroId)
        {
            using (ModelFirstContainer context = new ModelFirstContainer())
            {
                int userId = WebSecurity.CurrentUserId;
                User user = context.Users.SingleOrDefault(x => x.Id == userId);

                User targetUser = context.Users.SingleOrDefault(x => x.Id == targetBroId);

                RemoveBroFromCircle("MyBros", user, targetUser);
                RemoveBroFromCircle("MyBros", targetUser, user);
            }
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

        public void RemoveBroFromCircle(string circleName, User circleOwner, User broRemoved)
        {
            Circle targetCircle = GetCircleByName(circleOwner, circleName);
            if (targetCircle.Members.Contains(broRemoved))
            {
                targetCircle.Members.Remove(broRemoved);
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

		public ActionResult Post(int id)
		{

			return View();
		}

		public ActionResult Notifications()
		{
			List<Notification> notifications = new List<Notification>();
			using (ModelFirstContainer context = new ModelFirstContainer())
			{
				int userId = (int)Session["UserId"];

				foreach (Notification not in context.Notifications.Where(n => n.Receiver.Id == userId && n.IsRead == false))
				{
					if (not is CommentNotification)
					{
						CommentNotification cn = not as CommentNotification;
						context.Entry(cn.Comment.Owner).Reference(u => u.Profile).Load();
						notifications.Add(cn);
					}
					else if (not is Bros.DataModel.RequestNotification)
					{
						Bros.DataModel.RequestNotification cn = not as Bros.DataModel.RequestNotification;
						context.Entry(cn.BroRequest.Sender).Reference(u => u.Profile).Load();
						notifications.Add(cn);
					}
				}
			}

			return View(notifications);
		}

		[HttpPost]
		public ActionResult ReadNotification(int id)
		{
			using (ModelFirstContainer context = new ModelFirstContainer())
			{
				Notification not = context.Notifications.Where(n => n.Id == id).FirstOrDefault();

				if (not != null)
				{
					not.IsRead = true;
					context.SaveChanges();
				}
			}

			if (Request.IsAjaxRequest())
				return null;
			else
				return Redirect(Request.UrlReferrer.AbsolutePath);
		}

		[HttpPost]
		public ActionResult Bump(int id)
		{
			using (ModelFirstContainer context = new ModelFirstContainer())
			{
				int userId = (int)Session["UserId"];

				// get last bump between the current user and the specified user
				FirstBump lastBump = context.Notifications.Where(n => (n.Receiver.Id == id || n.Receiver.Id == userId) && n is FirstBump)
																.Select(n => n as FirstBump)
																	.Where(b => b.Sender.Id == userId || b.Sender.Id == id)
																		.OrderByDescending(b => b.DateCreated).FirstOrDefault();

				// if the last bump was sent by the user that I'm bumping or there has been no last bump
				if (lastBump == null && lastBump.Sender.Id == id)
				{
					User bumper = context.Users.Where(u => u.Id == userId).FirstOrDefault();
					User bumpee = context.Users.Where(u => u.Id == id).FirstOrDefault();

					FirstBump fistBump = new FirstBump() { DateCreated = DateTime.Now, Receiver = bumpee, Sender = bumper };

					context.SaveChanges();				
				}

			}

			if (Request.IsAjaxRequest())
				return null;
			else
				return Redirect(Request.UrlReferrer.AbsolutePath);
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
                    int userId = WebSecurity.CurrentUserId;
                    User user = context.Users.FirstOrDefault(u => u.Id == userId);
                    Comment comment = new Comment() { Content = Request["comment"], Owner = user, ParentPost = post, DateCreated = DateTime.Now };

					CommentNotification not = new CommentNotification() { Comment = comment, DateCreated = DateTime.Now, IsRead = false, Receiver = post.Author };

					comment.CommentNotifications.Add(not);
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
            User thisUser = new User();
            using (var context = new ModelFirstContainer())
            {
                int userId = WebSecurity.CurrentUserId;
                    thisUser = context.Users.Include("Profile").FirstOrDefault(u => u.Id == userId);


            }
                return View("ProfileAttribute", thisUser.Profile);
            
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
                        User user = context.Users.FirstOrDefault(u => u.Id == WebSecurity.CurrentUserId);
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

        [Authorize]
        public ActionResult MyProfile(int id)
        {
            User user = new User();
                using (var context = new ModelFirstContainer())
                {
                    user = context.Users.Include("Profile.ProfilePhoto").FirstOrDefault(u => u.Id == id);
                }
            return View(user);
        }

        [Authorize]
        [HttpGet]
        public ActionResult ChangeProfilePhoto()
        {
            User thisUser = new User();
            using (var context = new ModelFirstContainer())
            {
                thisUser = context.Users.Include("Profile.ProfilePhoto").FirstOrDefault(u => u.Id == WebSecurity.CurrentUserId);
            }
            return View(thisUser);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangeProfilePhoto(HttpPostedFileBase img)
        {

            User thisUser = new User();
            using (var context = new ModelFirstContainer())
            {
                thisUser = context.Users.Include("Profile.ProfilePhoto").FirstOrDefault(u => u.Id == WebSecurity.CurrentUserId);
                Album album = thisUser.Albums.FirstOrDefault(a => a.Title == "Profile Pictures");
                if (album == null)
                {
                    album = new Album()
                    {
                        Title = "Profile Pictures",
                        DateCreated = DateTime.Now,
                        Owner = thisUser,
                        IsDeleted = false
                    };
                }

                byte[] data = null;
                if (img != null && img.ContentLength > 0)
                {
                    using (MemoryStream target = new MemoryStream())
                    {                   
                        img.InputStream.CopyTo(target);
                        data = target.ToArray();
                              
                    }       
                }
                else
                {
                    throw new Exception("derpp");
                }    
                
                Photo photo = new Photo()
                        {
                            ImageData = data,
                            DateCreated = DateTime.Now,
                            IsDeleted = false,
                            IsFlagged = false,
                            Album = album,
                            UserId = thisUser.Id,
                            Caption = "",
                            DateUpdated = DateTime.Now
                            
                        };

                    album.Photos.Add(photo);
                    thisUser.Profile.ProfilePhoto = photo;  
                context.SaveChanges();
            }
            return View("Feed");
        }

        [Authorize]
        public ActionResult ManageAlbums()
        {
            List<Album> albumList = new List<Album>();
            using(var context = new ModelFirstContainer()){
                int id = WebSecurity.CurrentUserId;
                User user = context.Users.Include("Albums.Photos").FirstOrDefault(u => u.Id == id);
                albumList = user.Albums.ToList();
            }
            return View(albumList);
        }

        [Authorize]
        public ActionResult PhotoGallery(int id)
        {
            Album album = new Album();
            using(var context  = new ModelFirstContainer()){
                album = context.Albums.Include("Photos.Comments").FirstOrDefault(a => a.Id == id);
                ViewBag.AlbumId = album.Id;
            }
            return View(album.Photos.ToList());
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddPhoto(int albumId)
        {
            ViewBag.AlbumId = albumId;
            using(var context = new ModelFirstContainer()){
                Album album = context.Albums.Include("Owner").FirstOrDefault(u =>u.Id == albumId);
                int id = album.Owner.Id;
                ViewBag.UserId = id;
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddPhoto([Bind(Exclude = "ImageData")]Photo photo, HttpPostedFileBase img, int id, int AlbumId)
        {

            
             byte[] data = null;
            if (img != null && img.ContentLength > 0)
            {
                MemoryStream target = new MemoryStream();
                img.InputStream.CopyTo(target);
                data = target.ToArray();
            }
            else
            {
                throw new Exception("derpp");
            }

            using(var context = new ModelFirstContainer()){

                User user = context.Users.FirstOrDefault(u=> u.Id == id);
                Photo photo2 = new Photo()
                {
                    Caption = photo.Caption,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    ImageData = data,
                    AlbumId = AlbumId,
                    Author = user,
                    IsDeleted = false,
                    IsFlagged = false
 
                };

                context.Posts.Add(photo2);
                context.SaveChanges();
            }
            return RedirectToAction("PhotoGallery", "Profile", new { id = photo.AlbumId});
        }

        public ActionResult RemovePhoto(int id, int albumId)
        {
            using(var context = new ModelFirstContainer()){
                //Album album = context.Albums.FirstOrDefault(a =>a.Id == albumId);
                Photo photo = context.Posts.Include("Comments").Where(x => x is Bros.DataModel.Photo).Select(p => p as Bros.DataModel.Photo).FirstOrDefault(u => u.Id == id);

                if(photo.Comments.Count != 0){
                    //I need to cascade delete with photo & comments Get Felix's code for this
                }
                    
                context.Posts.Remove(photo);
                context.SaveChanges();
            }

            return RedirectToAction("PhotoGallery", "Profile", albumId);
        }

        public ActionResult SinglePhotoComment(int id)
        {
            Photo photo = new Photo();
            using(var context = new ModelFirstContainer()){
                photo = context.Posts.Include("Comments.Owner.Profile").Where(x => x is Bros.DataModel.Photo).Select(p => p as Bros.DataModel.Photo).FirstOrDefault(u => u.Id == id);
            }

            return View("PhotoComments", photo);
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateAlbum()
        {

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateAlbum(Album album)
        {

            int userId = WebSecurity.CurrentUserId;
            using(var context = new ModelFirstContainer()){
                            
                User own = context.Users.FirstOrDefault(u => u.Id == userId);
                Album al = new Album()
                {
                    Title = album.Title,
                    UserId = WebSecurity.CurrentUserId,
                    DateCreated = DateTime.Now,
                    IsDeleted = false,
                    Owner = own
                    
                };

                context.Albums.Add(al);
                try
                {
                    if (album.Title.ToLower().Equals("profile pictures")) throw new DbEntityValidationException();
                    context.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    ViewBag.ErrorMessage = "Cannot be named 'Profile Pictures'.";
                    return View();
                }
            }
            return RedirectToAction("ManageAlbums", "Profile");
        }

        public ActionResult DeleteAlbum(int id)
        {
            using(var context = new ModelFirstContainer()){
                int userId = WebSecurity.CurrentUserId;
                User owner = context.Users.FirstOrDefault(u => u.Id == userId);
                Album album = context.Albums.Include("Photos").FirstOrDefault(a => a.Id == id);
                
                album.Owner = owner;
                if (album.Photos.Count != 0){
                    foreach(Photo x in album.Photos.ToList()){
                        Photo photo = context.Posts.Include("Comments").Where(p => p is Bros.DataModel.Photo).Select(p => p as Bros.DataModel.Photo).FirstOrDefault(u => u.Id == x.Id);

                        if (photo.Comments.Count != 0)
                        {
                            foreach(Comment y in x.Comments.ToList()){
                                context.Comments.Remove(y);
                            }
                        }
                        context.Posts.Remove(photo);
                    }
                }

                context.Albums.Remove(album); 
                context.SaveChanges();
            }
            return RedirectToAction("ManageAlbums", "Profile");
        }
        

    }
}
