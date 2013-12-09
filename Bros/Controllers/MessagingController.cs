using Bros.DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Bros.Controllers
{
	[Authorize(Roles="User")]
    public class MessagingController : Controller
    {
        //
        // GET: /Messaging/

        public ActionResult MessageHome()
        {

            if (Session["UserId"] != null)
            {
                int id = (int)Session["UserId"];
                List<Message> allMessages;
                using (var context = new ModelFirstContainer())
                {
                    User thisUser = context.Users.FirstOrDefault(x => x.Id == id);
                    allMessages = thisUser.ReceivedMessages.ToList();
                    //allMessages.AddRange(thisUser.ReceivedMessages.ToList());

                    allMessages = allMessages.OrderBy(x => x.DateSent).ToList();
                    //List<User> uniqueUsers = allMessages.Select(m => m.Receiver).OrderBy(m => m.Id).Distinct().ToList();
                    List<User> uniqueUsers = context.Users.Include("Profile").ToList();
                  //List<User> uniqueUsers = context.Users.Include("Users.Profile").Where(x => x.SentMessages != thisUser.SentMessages) && allMessages.Select(m => m.Receiver).OrderBy(m => m.Id).Distinct().ToList();
                    ViewBag.messages = allMessages;

                    //ViewBag.UserConversations = uniqueUsers.Select(c => c.ReceivedMessages.Where(y => y.Receiver.Id == thisUser.Id)).Select(m => m.);
                    IEnumerable<int> messages = thisUser.SentMessages.Union(thisUser.ReceivedMessages).Select(m => m.Id);
                    List<User> userList = context.Users.Include("Profile").Where(u => u.Id != thisUser.Id).Where(u =>  u.SentMessages.Union(u.ReceivedMessages).Select(m => m.Id).Intersect(messages).Count() != 0 ).Distinct().ToList();

                    ViewBag.UserConversations = userList;
                }
                return View("MessageHome", allMessages);
            }
            else
            {
                throw new Exception("Session not found, or not logged in.");
                
            }
        }

        public ActionResult LoadMessage(int id)
        {
            if (Session["UserId"] != null)
            {
                int userId = (int)Session["UserId"];
                List<Message> allMessages = new List<Message>();
                using (var context = new ModelFirstContainer())
                {
                    User messenger = context.Users.FirstOrDefault(u => u.Id == id);
                    User thisUser = context.Users.FirstOrDefault(x => x.Id == userId);
                    allMessages = context.Messages.Include("Sender.Profile").Include("Receiver.Profile").Where(m => (m.Sender.Id == id && m.Receiver.Id == thisUser.Id)
                                                                                                                || (m.Sender.Id == thisUser.Id && m.Receiver.Id == id)).OrderBy(m => m.DateSent).ToList();
                    ViewBag.ReceiverId = id;
                }
                return View("_ConvoPartialView", allMessages);
            }
            else
                throw new Exception("Session is null, or user is not logged in.");
            
        }

        [HttpGet]
        public ActionResult CreateMessage()
        {
                using(var context = new ModelFirstContainer()){
                    int id = ((int)Session["UserId"]);
                User user = context.Users.Include("Profile").FirstOrDefault(x => x.Id == id );
                List<Circle> circleList = (List<Circle>)context.Circles.Where(x => x.Owner.Id == user.Id).ToList();
                //List<User> friends = user.Circles.Select(c => c.Members).SelectMany(u => u).ToList();

                    ViewBag.Users = user.Circles.Select(c => c.Members).SelectMany(u => u).Select(u => new { Id = u.Id, Name = u.Profile.FirstName + " " + u.Profile.LastName }).ToList();
                    ViewBag.Title = "Create Message";
                    ViewBag.Me = user;
                }
                return View("CreateMessage");
            
        }

        [HttpPost]
        public ActionResult PostCreateMessage(Message message)
        {
            int id = (int)Session["UserId"];
            int receiverId = Int32.Parse(Request["Receiver"]);
            using (var context = new ModelFirstContainer())
            {
                User thisUser = context.Users.FirstOrDefault(x => x.Id == id);
                Message ms = new Message(){
                    Content = message.Content,
                    DateSent = DateTime.Now,
                    DateRead = DateTime.Now,
                    Sender = thisUser,
                    Receiver = context.Users.Where(u => u.Id == receiverId).FirstOrDefault()
                };
                context.Messages.Add(ms);
                context.SaveChanges();

                IEnumerable<int> messages = thisUser.SentMessages.Union(thisUser.ReceivedMessages).Select(m => m.Id);
                List<User> userList = context.Users.Include("Profile").Where(u => u.SentMessages.Union(u.ReceivedMessages).Select(m => m.Id).Intersect(messages).Count() != 0).ToList();

                ViewBag.UserConversations = userList;
            }

           
            //if (ModelState.isValid)
            //{

            //}
            

            return View("MessageHome");
        }

		public ActionResult Chat()
		{
			using (var context = new ModelFirstContainer())
			{
				IEnumerable<int> friends = context.Users.FirstOrDefault(u => u.Id == WebSecurity.CurrentUserId)
													.Circles.Select(c => c.Members).SelectMany(u => u).Select(u => u.Id);

				ViewBag.Friends = context.Users.Include("Profile").Where(u => friends.Contains(u.Id)).ToList();
				ViewBag.Name = context.Users.Where(u => u.Id == WebSecurity.CurrentUserId).Select(u => u.Profile.FirstName + " " + u.Profile.LastName).FirstOrDefault();
			}

			ViewBag.UserId = WebSecurity.CurrentUserId;

			return View();
		}

    }
}
