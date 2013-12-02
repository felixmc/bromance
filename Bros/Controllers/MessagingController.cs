using Bros.DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bros.Controllers
{
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
                    List<User> uniqueUsers = allMessages.Select(m => m.Receiver).OrderBy(m => m.Id).Distinct().ToList();
                    ViewBag.messages = allMessages;
                    ViewBag.UserConversations = uniqueUsers;
                }
                return View("MessageHome", allMessages);
            }
            else
            {
                throw new Exception("Session not found, or not logged in.");
                
            }
        }

        public ActionResult LoadMessage(User u)
        {
            Bros.DataModel.User thisUser = (Bros.DataModel.User)Session["User"];
            List<Message> allMessages = thisUser.SentMessages.ToList();
            allMessages.AddRange(thisUser.ReceivedMessages.ToList());
            allMessages = allMessages.Where(x => u.Id == x.Receiver.Id || x.Sender.Id == u.Id).OrderBy(x => x.DateSent).ToList();

            return View();
        }
    }
}
