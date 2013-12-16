using Bros.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Bros.Controllers
{
	[Authorize(Roles="User")]
    public class ContentController : BroController
    {

		[HttpPost]
		public ActionResult PostStatus()
		{
			using (ModelFirstContainer context = new ModelFirstContainer())
			{
				int userId = WebSecurity.CurrentUserId;
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

		public ActionResult Post(int id)
		{
			Post post = null;

			using (ModelFirstContainer context = new ModelFirstContainer())
			{
				post = context.Posts.Include("Comments.Owner.Profile").Include("Author.Profile").FirstOrDefault(p => p.Id == id);
			}

			return View(post);
		}

		[HttpPost]
		public ActionResult PostComment()
		{
			using (ModelFirstContainer context = new ModelFirstContainer())
			{
				int postId = Int32.Parse(Request["postId"]);
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
                thisUser.Profile.ProfilePhoto.Caption = (string)Request["caption"];
                context.SaveChanges();

                ViewBag.Message = "Profile Photo was updated successfully!";
            }
            return View(thisUser);
        }

		public ActionResult ManageAlbums()
		{
			List<Album> albumList = new List<Album>();
			using (var context = new ModelFirstContainer())
			{
				int id = WebSecurity.CurrentUserId;
				User user = context.Users.Include("Albums.Photos").FirstOrDefault(u => u.Id == id);
				albumList = user.Albums.ToList();
			}

            ViewBag.Title = "Manage Albums";
            ViewBag.SubTitle = "Manage Albums";
			return View(albumList);
		}

		public ActionResult PhotoGallery(int id)
		{
			Album album = new Album();
			using (var context = new ModelFirstContainer())
			{
				album = context.Albums.Include("Photos.Comments").FirstOrDefault(a => a.Id == id);
				ViewBag.AlbumId = album.Id;
			}

            @ViewBag.Title = "Photo Gallery";
			return View(album.Photos.ToList());
		}

		[HttpGet]
		public ActionResult AddPhoto(int albumId)
		{
			ViewBag.AlbumId = albumId;
			using (var context = new ModelFirstContainer())
			{
				Album album = context.Albums.Include("Owner").FirstOrDefault(u => u.Id == albumId);
				int id = album.Owner.Id;
				ViewBag.UserId = id;
			}

            ViewBag.Title = "Upload Photo";
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

			using (var context = new ModelFirstContainer())
			{

				User user = context.Users.FirstOrDefault(u => u.Id == id);
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
			return RedirectToAction("PhotoGallery", "Content", new { id = photo.AlbumId });
		}

		public ActionResult RemovePhoto(int id, int albumId)
		{
			using (var context = new ModelFirstContainer())
			{
				//Album album = context.Albums.FirstOrDefault(a =>a.Id == albumId);
				Photo photo = context.Posts.Include("Comments").Where(x => x is Bros.DataModel.Photo).Select(p => p as Bros.DataModel.Photo).FirstOrDefault(u => u.Id == id);

				if (photo.Comments.Count != 0)
				{
					//I need to cascade delete with photo & comments Get Felix's code for this
				}

				context.Posts.Remove(photo);
				context.SaveChanges();
			}

			return RedirectToAction("PhotoGallery", "Content", albumId);
		}

		[HttpGet]
		public ActionResult CreateAlbum()
		{
            @ViewBag.Title = "Create album";
			return View();
		}

		[HttpPost]
		public ActionResult CreateAlbum(Album album)
		{

			int userId = WebSecurity.CurrentUserId;
			using (var context = new ModelFirstContainer())
			{

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
			return RedirectToAction("ManageAlbums", "Content");
		}

		public ActionResult DeleteAlbum(int id)
		{
			using (var context = new ModelFirstContainer())
			{
				int userId = WebSecurity.CurrentUserId;
				User owner = context.Users.FirstOrDefault(u => u.Id == userId);
				Album album = context.Albums.Include("Photos").FirstOrDefault(a => a.Id == id);

				album.Owner = owner;
				if (album.Photos.Count != 0)
				{
					foreach (Photo x in album.Photos.ToList())
					{
						Photo photo = context.Posts.Include("Comments").Where(p => p is Bros.DataModel.Photo).Select(p => p as Bros.DataModel.Photo).FirstOrDefault(u => u.Id == x.Id);

						if (photo.Comments.Count != 0)
						{
							foreach (Comment y in x.Comments.ToList())
							{
								context.Comments.Remove(y);
							}
						}
						context.Posts.Remove(photo);
					}
				}

				context.Albums.Remove(album);
				context.SaveChanges();
			}
			return RedirectToAction("ManageAlbums", "Content");
		}

        public ActionResult ViewAlbums(int id)
        {
            
            User user = new User();
            using (var context = new ModelFirstContainer())
            {
               user  = context.Users.Include("Albums.Photos").FirstOrDefault( u => u.Id == id);
                
            }


            return View(user);
        }

        public ActionResult SingleAlbum(int id)
        {
            Album album = new Album();
            using (var context = new ModelFirstContainer())
            {
                album = context.Albums.Include("Photos.Comments").FirstOrDefault(a => a.Id == id);
                ViewBag.AlbumId = album.Id;
            }

            @ViewBag.Title = "Photo Gallery";
            return View(album.Photos.ToList());
        }

    }
}